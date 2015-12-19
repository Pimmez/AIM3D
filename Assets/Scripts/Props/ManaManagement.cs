using UnityEngine;
using System.Collections;

public class ManaManagement : InputOptions {

	private ManaBar manabar;
	[SerializeField]
	private float manaCost;

	//GetInput will be activated when you press a button (M in particular)
	protected override void GetInput()
	{
		base.GetInput ();
		print ("Acces ManaManagement Script");

		//manabar = GameObject.Find ("obj").GetComponent<Manabar> ();

		manabar = GameObject.Find ("Player").GetComponent<ManaBar> ();

		//currentMana = GetComponent<ManaBar> ();
		//manaCost = GetComponent<ManaBar> ();


		//If the current mana isnt more then the cost of the object
		if (ManaBar.currentMana >= ManaBar.manaCost) 
		{
			print (ManaBar.currentMana);

			print ("We Have EnoughMana");
			ManaBar.currentMana -= 10.0f;
			print (ManaBar.currentMana);

			//manabar.UseMana(manaCost);

			EnoughMana ();

		} 
		else
		{	
			//When there is not enough mana
			print ("No Mana Available");
		}
	}


	protected virtual void EnoughMana()
	{
		print ("Entered EnoughMana Function");

	}
}
