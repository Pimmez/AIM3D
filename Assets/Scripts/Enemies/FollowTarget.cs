using UnityEngine;
using System.Collections;

public class FollowTarget : CheckForTarget {

    [SerializeField]
    private float followSpeed;

    //the minimal distance to reach a waypoint, and return its patrol.
    private float minDistanceToPoint;

    private bool seeTarget;

    private NavMeshAgent agent;

    private GoToPointSmooth goToPointSmooth;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //set the navmesh speed
        agent.speed = followSpeed;

        //start with the navmesh inactive
        agent.Stop();

        goToPointSmooth = GetComponent<GoToPointSmooth>();
    }

    public override void SeeTarget(Transform _targetTransform)
    {
        base.SeeTarget(_targetTransform);

        //stop patrolling
        goToPointSmooth.setPatrolling(false);

        //set seetarget true, so the target transform updates as long as see target is true
        seeTarget = true;

        //stop the coroutine that updates the target position in the navmesh
        StartCoroutine(UpdateTargetTranformInNavmesh(_targetTransform));

        //follow the target
        agent.Resume();
    }

    protected override void LoseCurrentTarget()
    {
        base.LoseCurrentTarget();

        //if there was a target
        if (seeTarget)
        {
            //stop the coroutine that updates the target position in the navmesh
            seeTarget = false;

            //set the navmesh target to the last waypoint
            agent.destination = goToPointSmooth.Point;

            //going back to the last waypoint
            StartCoroutine(GoToPoint());
        }
    }

    private IEnumerator UpdateTargetTranformInNavmesh(Transform _targetTransform)
    {
        while (seeTarget) {
            //set the new position of the target
            agent.destination = _targetTransform.position;

            //set the rotation to the target
            transform.LookAt(_targetTransform);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator GoToPoint()
    {
        //while it hasnt reached the last point, keep going to the last point
        while (!goToPointSmooth.CheckDistanceToPoint())
        {
            yield return new WaitForFixedUpdate();
        }

        //stop the navmesh
        agent.Stop();

        //update the position value of the position it is now;
        goToPointSmooth.UpdatePostion();

        //and resume patrolling
        goToPointSmooth.setPatrolling(true);
    }
}
