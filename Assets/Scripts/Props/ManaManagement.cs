using UnityEngine;
using System.Collections;

public class ManaManagement : InputOptions {

	private ManaBar currentMana;

	protected override void Awake()
	{
		base.Awake ();
		currentMana = GameObject.Find("ManaBarSprite").GetComponent<ManaBar>();
	}

	//GetInput will be activated when you press a button (M in particular)
	protected override void GetInput()
	{
		base.GetInput ();
		print ("Acces ManaManagement Script");

		if (currentMana.UseMana (10.0f))
		{
			EnoughMana ();
		} 
		else
		{
			print ("No Mana the object to move");
		}
	}

	protected virtual void EnoughMana()
	{
		print ("Entered EnoughMana Function");
	}
}
