using UnityEngine;
using System.Collections;

public class InputOptions : CheckIfInCamera {
	
	private bool canPress;

	//When object is visible activited StartCoroutine
	protected override void Visible ()
	{
		base.Visible ();
		if (!canPress)
		{
			//sets the boolean to true when its visible
			canPress = true;
			StartCoroutine(WaitForInput());
		}
	}
	
	protected override void NotVisible ()
	{
		//When its not visible the coroutine cant be used
		base.NotVisible ();
		canPress = false;
		StopAllCoroutines();
	}
	
	protected virtual void GetInput()
	{
		
	}

	//IEnumerator  that will give you the input, the boolean will automaticly set false when its over.
	IEnumerator WaitForInput()
	{
		while (canPress) 
		{
			if (Input.GetKeyDown("m"))
			{
				GetInput();
			}
			yield return null;
		}
		canPress = false;
	}
}