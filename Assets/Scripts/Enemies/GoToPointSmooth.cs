using UnityEngine;

public class GoToPointSmooth : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 0.03f;

    /** The heavyier the object, the more momentum it has while steering */
    [SerializeField]
    private float mass = 100;

    /** the boolean to check if the obj needs to look the directions it is moving */
    [SerializeField]
    private bool rotateToDirection = true;

    /** the boolean to check if we need to follow the waypoints, of if another is going to move this object */
    [SerializeField]
    private bool patrolling = true;

    [SerializeField]
    private float minDistanceToPoint = 1;

    /** Vector to save the current velocity, X & Z */
    private Vector3 currentVelocity;
    /** Vector to save the current position, X & Z */
    private Vector3 currentPosition;
    /** Vector om de locatie bij te houden waar we heen willen */
    /** Vector to save the position of the current target*/
    private Vector3 currentPoint;

    void Start()
    {
        // we starten zonder beweging (geen velocity)
        currentVelocity = new Vector3();
        // Assign currentPosition to the start position of this object
        currentPosition = transform.position;
    }

    // Seek every frame, to see if we need to adjust our movement
    void Update()
    {
        if (patrolling) Seek();
    }

    public void setPatrolling(bool _patrolState)
    {
        patrolling = _patrolState;
    }

    public bool Patrolling
    {
        get { return patrolling; }
    }

    public void setPoint(Vector3 point)
    {
        currentPoint = point;
    }

    // Get the target V3 from another script
    public Vector3 Point
    {
        get { return currentPoint; }
    }

    public void UpdatePostion()
    {
        currentPosition = transform.position;
    }

    void Seek()
    {
        // Calculate the distance from our current position to our current target
        Vector3 desiredStep = currentPoint - currentPosition;

        // DisiredStep cant be bigger than the maximal speed
        // If the vector is normalized, it keeps the same direction but the length/magnitude is 1
        desiredStep.Normalize();

        // Multiply the desiredStep by maxSpeed and save it as desiredVelocity
        Vector3 desiredVelocity = desiredStep * maxSpeed;

        // Calculate what the vector must be to adjust the direction and reach disiredVelocity
        Vector3 steeringForce = desiredVelocity - currentVelocity;

        // increment the currentVelocity by the steering force, but first divide it by mass, so the bigger the mass, the lower the steeringforce
        currentVelocity += steeringForce / mass;

        // Update our actual position by incrementing it with our velocity
        currentPosition += currentVelocity * Time.deltaTime;
        transform.position = currentPosition;

        // adjust the rotation to the movement, if needed.
        if (rotateToDirection)
        {
            float angle = Mathf.Atan2(currentVelocity.z, currentVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.down);
        }
    }

    //checks if the distance between this object and the currentPoint is lower then minDistanceToPoint
    public bool CheckDistanceToPoint()
    {
        //check if the distance is lower than the minimal distance to the point
        if (Vector3.Distance(transform.position, currentPoint) < minDistanceToPoint) return true;
        else return false;
    }
}