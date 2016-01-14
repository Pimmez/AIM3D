using UnityEngine;

public class CheckCameraVisible : WaitForCheck
{
    [SerializeField]
    private bool checkWithRayCast = true;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float angle = 180;

    [SerializeField]
    private float distance = 10;

    private RaycastSight raycastSight;

    protected override void Start()
    {
        base.Start();
        raycastSight = GetComponent<RaycastSight>();
    }

    protected override void Check()
    {
        base.Check();
        
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        //if i am in the camera
        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds))
        {
            //if we need to check for raycast
            if (checkWithRayCast)
            {
                CheckRaycast();
            } else { //else we just check the distane
                CheckDistance();
            }
        }
        else
        {
            NotVisible();
        }
    }

    private void CheckRaycast() {
        //if the the target is in front of us
        if (raycastSight.CheckRaycastSight(transform, target, angle, distance))
        {
            Visible();
        }
    }

    private void CheckDistance()
    {
        //if we are close enough to the target
        if (Vector3.Distance(transform.position, target.position) < distance)
        {
            Visible();
        }
    }

    protected virtual void Visible()
    {


    }

    protected virtual void NotVisible()
    {


    }
}
