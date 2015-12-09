using UnityEngine;
using System.Collections;

public class CheckIfInView : MonoBehaviour {

    void FixedUpdate()
    {
        Plane[]  planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds)) Debug.Log("Object inside frustum");
        else Debug.Log("Object not visible");
    }

	protected virtual void InView()
	{

	}
}
