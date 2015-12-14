using UnityEngine;
using System.Collections;

public class InputOptions : CheckIfInCamera {
	
	private bool pressed;

	protected override void Visible ()
	{
		print ("Visable");
		base.Visible ();
		if(!pressed)StartCoroutine (WaitForInput());

	}

	protected override void NotVisible ()
	{
		print ("NotVisable");
		base.NotVisible ();
		pressed = false;
	}

	protected virtual void GetInput()
	{

	}

	IEnumerator WaitForInput()
	{
		print ("Waitforinput");

		while (!pressed) 
		{
			//print ("While false");

			if(Input.GetKeyDown("e"))
			{
				print ("pressed");

				pressed = true;
				GetInput();
			}
		}
			yield return null;
	}
}