using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ManaBar : MonoBehaviour {
	[SerializeField]
	private float maxMana = 100;
	
	[SerializeField]
	private float currentManaVal = 50;
	
	private float oldManaVal;
	
	[SerializeField]
	private float smoothTime = 0f;

	public bool UseMana(float _manaCost) 
	{
		//If The cost of the object is lower then the current mana we have, only then you can go through.
		if (currentManaVal >= _manaCost)
		{
			print (currentManaVal);
			
			print ("We Have EnoughMana");
			
			ChangeMana(-_manaCost);
			
			return true;
		}
		else
		{ 
			//When there is not enough mana.
			print ("No Mana Available");
			return false;
		}
	}
	
	public void ChangeMana(float change) {
		//we save the old mana value
		oldManaVal = currentManaVal;
		
		//and then change the current value
		currentManaVal += change;
	}
	
	void FixedUpdate()
	{
		float velocity = new float();
		
		float bar = Mathf.SmoothDamp (oldManaVal, currentManaVal, ref velocity, smoothTime);
		
		Vector3 temp = transform.localScale;
		temp.x = bar;
		transform.localScale = temp;
	}
	
	public void StartManaDraining(float _costPerFrame) {
		StartCoroutine(DrainMana(_costPerFrame));
		
	}
	
	public void StopManaDraining()
	{
		StopCoroutine("DrainMana");
	}
	
	private IEnumerator DrainMana(float _cost)
	{
		//while we are scaling the parent character/player
		while (currentManaVal > 0)
		{
			ChangeMana(-_cost);
			yield return new WaitForFixedUpdate();
		}
	}
}