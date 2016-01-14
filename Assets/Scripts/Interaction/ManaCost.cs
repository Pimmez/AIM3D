using UnityEngine;
using System.Collections;

public class ManaCost : CheckVisibility {

    [SerializeField]
    private int manaCost = 15;

    [SerializeField]
    private GameObject ManaEffect;

    [SerializeField]
    private int NotEnoughManaEffectOffTime = 35;

	private ManaBar currentMana;

    [SerializeField]
    private bool onlyUsableOnce;

    private bool usable = true;

    protected override void Awake()
    {
        base.Awake();
        currentMana = GameObject.Find("ManaBar").GetComponent<ManaBar>();
    }

    //GetInput will be activated when you press a button (M in particular)
    protected override void Visible()
	{
		base.Visible();

        if (usable)
        {
            //do a check if we have enough mana, if true we decrement the mana at the same tim
            if (currentMana.UseMana(manaCost))
            {
                //if we can only use this object once, set useAble in check input false
                if (onlyUsableOnce)
                {
                    usable = false;

                    ManaEffect.SetActive(false);
                }
                EnoughMana();
            }
            else
            {
                print("No Mana the object to move");
                StartCoroutine(FlickerManaEffect());
            }
        }
	}

	protected virtual void EnoughMana()
	{

	}

    IEnumerator FlickerManaEffect()
    {
        ManaEffect.SetActive(false);

        for (int i = NotEnoughManaEffectOffTime; i > 0; i--)
        {
            yield return new WaitForFixedUpdate();
        }

        ManaEffect.SetActive(true);
    }
}
