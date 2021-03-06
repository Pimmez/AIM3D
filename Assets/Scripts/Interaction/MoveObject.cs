﻿using UnityEngine;
using System.Collections;

public class MoveObject : ManaCost {

	[SerializeField]
	private Transform endPos;
	[SerializeField]
	private float smoothTime = 3;
	[SerializeField]
	private GameObject obj;

    [SerializeField]
    private float minDistance = 0.5f;

	//Void EnoughMana Only Activates when We have enough mana.
	protected override void EnoughMana()
	{
		base.EnoughMana ();
		StartCoroutine (moveObject(endPos.position));
	}

	//Using an IEnumerator to move only move an object when it starts the coroutine
	IEnumerator moveObject(Vector3 goal)
	{
		Vector3 velocity = new Vector3();
		while (Vector3.Distance(goal, obj.transform.position) > minDistance) 
		{
            obj.transform.position = Vector3.SmoothDamp (obj.transform.position, goal, ref velocity, smoothTime);
			yield return new WaitForFixedUpdate ();
		}
	} 
}