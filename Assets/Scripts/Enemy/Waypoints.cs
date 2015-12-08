using UnityEngine;
using System.Collections.Generic;

// deze Class kan functies aanroepen in SteeringVehicle.cs
public class WaypointsFollower : MonoBehaviour
{

    [SerializeField]
    private float minDist;

    // todo: zorg ervoor dat dit component een lijst met waypoints/Vectors kan bevatten (instelbaar vanuit de editor)
    [SerializeField]
    private List<Transform> waypoints;

    private int waypointIndex;

    private GoToPointSmooth steering;

    void Awake()
    {
        // todo: als er al waypoints beschikbaar zijn: ga richting de eerste waypoint
        steering = GetComponent<GoToPointSmooth>();
        steering.setTarget(waypoints[waypointIndex].position);
    }

    void Update()
    {
        // todo: checken of we al in de buurt zijn van de eerstvolgende waypoint: zo ja -> ga door naar het volgende waypoint (setTarget() op SteeringVehicle.cs)
        if (Vector3.Distance(transform.position, steering.Target) < minDist) NextPoint();
    }

    // zorg ervoor dat er een addWayPoint method is
    private void NextPoint()
    {
        if (waypointIndex < waypoints.Count - 1) waypointIndex++;
        else waypointIndex = 0;
        steering.setTarget(waypoints[waypointIndex].position);
    }
}
