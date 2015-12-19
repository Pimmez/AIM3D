using UnityEngine;
using System.Collections;
using System;

public class ManaBar : MonoBehaviour {

	public static float currentMana = 10.0f;
	public Texture2D manaTexture;
	private float maxMana = 100.0f;
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

	public void UseMana(int price) {
		print ("manabar");
	}

	void Update()
	{
		percentOfMana = currentMana/maxMana;
		manaBarLenght = percentOfMana*100;

	}

}