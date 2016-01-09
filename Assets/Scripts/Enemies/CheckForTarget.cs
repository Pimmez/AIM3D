using UnityEngine;

public class CheckForTarget : WaitForCheck
{
    [SerializeField]
    protected string checkTag = "Player"; // which tag is the target

    [SerializeField]
    private float seeRange; // how far i can spot the target

    [SerializeField]
    private float fieldOfView = 80; // how wide i can spot the target

    [SerializeField]
    private int maxOutOfSightTime; // the max time the target has been out of sight

    private float outOfSightTime; // the time the target has been out of sight

    private GameObject target; //store the target we can see

    // we dont do a raycast check for the target if targetInRange is false,
    // but we still want to count the time we havent seen the player 
    private bool targetInRange;

    private RaycastSight raycastSight;

    protected override void Awake()
    {
        base.Awake();
        //adjust outofsight time to the ckeckcooldown
        outOfSightTime *= checkCooldown;

        raycastSight = GetComponent<RaycastSight>();
    }

    void OnTriggerStay(Collider other) {
        if (other.CompareTag(checkTag) && !targetInRange)
        {
            // the target in our range gets stored in target variable.
            target = other.gameObject;

            //add myself to the target noise list (the targets sound can now reach me)
            other.GetComponentInParent<Noise>().enemiesThatCanHearMe.Add(gameObject);

            //start checking sight for the target.
            targetInRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag(checkTag) && targetInRange)
        {
            // remove myself from the targets noise list (the targets sound can no longer reach me)
            RemoveFromNoiseList(other.gameObject);

            //stop checking the sight for the target until the he is in our range again, we keep following until the outOfSightTime is over.
            targetInRange = false;
        }
    }

    public void RemoveFromNoiseList(GameObject plr) {
        // remove myself from the targets noise list (the targets sound can no longer reach me)
        plr.GetComponentInParent<Noise>().enemiesThatCanHearMe.Remove(gameObject);
    }

    protected override void Check()
    {
        base.Check();

        // start with seetarget false, becomes true if we see the player
        bool seeTarget = false;

        // if the target is in range (we will never see the target if it isnt in our range)
        if (targetInRange)
        {
            if (raycastSight.CheckRaycastSight(transform, target.transform, fieldOfView, seeRange)) {
                seeTarget = true;
                SeeTarget(target.transform);
            }
        }

        if (!seeTarget)
        {
            //every time i dont see the target, increment out of sight time to know how long we didnt see the player
            outOfSightTime++;
            if (outOfSightTime >= maxOutOfSightTime) LoseCurrentTarget();
        }
    }

    public virtual void SeeTarget(Transform _target) {
        //every time we see the target, we reset the cooldown before the we lose the target.
        outOfSightTime = 0;
    }

    public virtual void LoseCurrentTarget() {

    }

    public GameObject Target {
        get { return target; }
    }

    public void DestroyMyself() {
        //remove myself from player sound list
        RemoveFromNoiseList(GetComponent<CheckForTarget>().Target);

        //then destroy the gameobject
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);

        //visualize the seeRange length
        Vector3 direction = transform.TransformDirection(Vector3.forward) * seeRange;
        Gizmos.DrawRay(transform.position, direction);
    }
}
