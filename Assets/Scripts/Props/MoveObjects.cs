using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveObjects : ManaManagement {

	[SerializeField]
	private Transform endPos;
	[SerializeField]
	private float smoothTime = 0f;
	[SerializeField]
	private GameObject obj;

	//Void EnoughMana Only Activates when We have enough mana.
	protected override void EnoughMana()
	{
		base.EnoughMana ();
		print ("StartCoroutine");
		StartCoroutine (moveObject(endPos.position));	

	}

	//Using an IEnumerator to move only move an object when it starts the coroutine
	IEnumerator moveObject(Vector3 goal)
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