using UnityEngine;
using System.Collections;
using System;

public class ManaBar : MonoBehaviour {


	static float currentMana = 100.0f;
	static float maxMana = 100.0f;
	public Texture2D manaTexture;
	private float manaBarLenght;
	private float percentOfMana;


	void OnGUI()
	{
		//Makes the GUI disapeare when it hits 0
		if (currentMana > 0) 
		{
			GUI.DrawTexture( new Rect((Screen.width/4) - 100, 25, manaBarLenght, 20), manaTexture);
		}
	}

	void Update()
	{
		percentOfMana = currentMana/maxMana;
		manaBarLenght = percentOfMana*100;

		//if pressed M, mana will decrease.
		if (Input.GetKeyDown ("m")) 
		{
			currentMana -= 5.0f;
		}
	}

}