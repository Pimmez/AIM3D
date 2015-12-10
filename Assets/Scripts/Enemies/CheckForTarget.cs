using UnityEngine;
using System.Collections;

public class CheckForTarget : WaitForCheck
{
    [SerializeField]
    protected LayerMask checkLayer;

    [SerializeField]
    private float seeRange;

    [SerializeField]
    private float seeRadius;

    [SerializeField]
    private float maxHearRange;

    [SerializeField]
    private int maxOutOfSightTime;

    private int outOfSightTime;

    protected override void Check()
    {
        base.Check();

        RaycastHit hit;

        //check a cone in front of this object for other objects with a certain layer
        if (Physics.SphereCast(transform.position, seeRadius, transform.forward, out hit, seeRange, checkLayer))
        {
            SeeTarget(hit.transform);
            outOfSightTime = 0;
        }
        else
        {
            outOfSightTime++;
            if (outOfSightTime >= maxOutOfSightTime) LoseCurrentTarget();
        }
    }

    public virtual void SeeTarget(Transform _target) {

    }

    protected virtual void LoseCurrentTarget() {

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, maxHearRange);

        Vector3 direction = transform.TransformDirection(Vector3.forward) * seeRange;
        Gizmos.DrawRay(transform.position, direction);
    }
}