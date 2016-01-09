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

    private AudioSource seeTargetSound;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();

        //set the navmesh speed
        agent.speed = followSpeed;

        //start with the navmesh inactive
        agent.enabled = false;

        goToPointSmooth = GetComponent<GoToPointSmooth>();

        //get all audiosource components on this object, save them in a array
        AudioSource[] audioSources = GetComponents<AudioSource>();

        //assign the sound we play when we see our target (2nd audiosource) to seeTargetSound
        seeTargetSound = audioSources[1];
    }

    public override void SeeTarget(Transform _targetTransform)
    {
        base.SeeTarget(_targetTransform);

        //if see target, this is the first time the player is detected since patrolling.
        if (!seeTarget)
        {
            //play sound
            seeTargetSound.Play();

            //stop patrolling
            goToPointSmooth.setPatrolling(false);

            //set seetarget true, so the target transform updates as long as see target is true
            seeTarget = true;

            //follow the target
            agent.enabled = true;

            //stop the coroutine that updates the target position in the navmesh
            StartCoroutine(UpdateTargetTranformInNavmesh(_targetTransform));
        }
    }

    public override void LoseCurrentTarget()
    {
        base.LoseCurrentTarget();

        //if there was a target
        if (seeTarget)
        {
            //stop the coroutine that updates the target position in the navmesh
            seeTarget = false;

            if (goToPointSmooth.Point != new Vector3(0,0,0)) {//set the navmesh target to the last waypoint
                agent.destination = goToPointSmooth.Point;
            }
            else {//else the monster wil be destroyed.
                DestroyMyself();
            }

            //going back to the last waypoint
            StartCoroutine(GoToPoint());
        }
    }

    private IEnumerator UpdateTargetTranformInNavmesh(Transform _targetTransform)
    {
        while (seeTarget) {
            //set the new position of the target
            agent.destination = _targetTransform.position;

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
        agent.enabled = false;

        //update the position value of the position it is now;
        goToPointSmooth.UpdatePostion();

        //and resume patrolling
        goToPointSmooth.setPatrolling(true);
    }
}
