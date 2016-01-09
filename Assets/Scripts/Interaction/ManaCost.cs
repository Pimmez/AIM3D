using UnityEngine;

public class ManaCost : CheckInput {

    [SerializeField]
    private int manaCost = 15;

	private ManaBar currentMana;

	protected override void Awake()
	{
        base.Awake();
		currentMana = GameObject.Find ("ManaBar").GetComponent<ManaBar>();
	}

	//GetInput will be activated when you press a button (M in particular)
	protected override void InputRecieved()
	{
		base.InputRecieved();

        //do a check if we have enough mana
        if (currentMana.UseMana(manaCost)) 
		{
            //if we can only use this object once, set useAble in check input false
            if (onlyUsableOnce)
            {
                useAble = false;
            }

            EnoughMana ();
		} 
		else
		{
			print ("No Mana the object to move");
		}
	}

	protected virtual void EnoughMana()
	{

	}
}
