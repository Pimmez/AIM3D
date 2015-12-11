using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveWalls : MonoBehaviour {

	[SerializeField]
	private Transform endPos;
	[SerializeField]
	private float smoothTime = 0f;
	[SerializeField]
	private GameObject obj;

	void Awake ()
	{
		StartCoroutine (moveWalls(endPos.position));	
	}

	IEnumerator moveWalls(Vector3 goal)
	{
		Vector3 velocity = new Vector3();
		while (Vector3.Distance(goal, obj.transform.position) > 0.5f) 
		{
			//Vector3 SmoothDamp(currentPos, endPos, ref Vector3 currentVelocity, smoothTime);
			transform.position = Vector3.SmoothDamp (obj.transform.position, goal, ref velocity, smoothTime);
			yield return new WaitForFixedUpdate ();
		}
	} 
}