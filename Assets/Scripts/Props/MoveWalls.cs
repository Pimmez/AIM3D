using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveWalls : MonoBehaviour {
	/*
	[SerializeField]
	private Vector3 endPos;
	[SerializeField]
	private float smoothTime = 0f;
	private GameObject gameobject;

	void  Start ()
	{
		gameobject = this.gameobject;
		StartCoroutine (moveWalls(5f));	

	}

	IEnumerator moveWalls(float waitTime)
	{
		Vector3 velocity = new Vector3 ();
		while (gameobject.transform.position > 0.5f) 
		{
			//Vector3 SmoothDamp(currentPos, endPos, ref Vector3 currentVelocity, smoothTime);
			transform.position = Vector3.SmoothDamp(gameobject.transform.position, endPos, ref velocity, smoothTime * Time.deltaTime);
		}

		yield return new WaitForSeconds (waitTime);
	}
*/
}
