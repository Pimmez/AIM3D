using UnityEngine;
using System.Collections;

public class ManaManagement : InputOptions {
	

	protected override void GetInput()
	{
		base.GetInput ();
		print ("Acces ManaManagement Script");
		EnoughMana ();
	}


	protected virtual void EnoughMana()
	{
		print ("Entered EnoughMana Function");
	}
}
