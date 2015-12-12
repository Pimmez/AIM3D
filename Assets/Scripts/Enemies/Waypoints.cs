using UnityEngine;
using System.Collections.Generic;

// deze Class kan functies aanroepen in SteeringVehicle.cs
public class Waypoints : MonoBehaviour
{
    // todo: zorg ervoor dat dit component een lijst met waypoints/Vectors kan bevatten (instelbaar vanuit de editor)
    [SerializeField]
    private List<Transform> waypoints;

    private int waypointIndex;

    private GoToPointSmooth goToPointSmooth;

    void Awake()
    {
        // go to the first waypoint
        goToPointSmooth = GetComponent<GoToPointSmooth>();
        goToPointSmooth.setPoint(waypoints[waypointIndex].position);
    }

    void Update()
    {
        // if the distance between a p
        if (goToPointSmooth.Patrolling && goToPointSmooth.CheckDistanceToPoint()) NextPoint();
    }

    //set the next, or first waypoint in the array as target in followpointsmooth.
    private void NextPoint()
    {
        if (waypointIndex < waypoints.Count - 1) waypointIndex++;
        else waypointIndex = 0;
        goToPointSmooth.setPoint(waypoints[waypointIndex].position);
    }
}
