using UnityEngine;

public class RaycastSight : MonoBehaviour {

    public bool CheckRaycastSight(Transform _myself, Transform _target, float _fov, float _range) {

        bool canSee = false;

        // make a vector that stores the distance from myself and the target
        Vector3 direction = _target.position - _myself.position;

        // calculate the angle between the target and our sight (we look forward).
        float angle = Vector3.Angle(direction, _myself.forward);

        // if the angle is in our field of view
        if (angle < _fov * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(_myself.position + _myself.up, direction.normalized, out hit, _range))
            {
                //if hit is the target and not something else
                if (hit.collider.transform == _target)
                {
                    canSee = true;
                }
            }
        }

        return canSee;
    }
}
