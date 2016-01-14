using UnityEngine;

public class GivePlayerMana : CheckTriggerEnterTag
{
    [SerializeField]
    private int manaValue;

    private ManaBar manaBar;

    void Start() {
        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<ManaBar>();
    }

    protected override void OnTriggerEnterWithTag()
    {
        base.OnTriggerEnterWithTag();

        GiveMana();
    }

    private void GiveMana()
    {
        //add mana to player manabar

        if (manaBar.CurrentManaVal < manaBar.MaxMana)
        {
            //increase the players mana
            manaBar.ChangeMana(manaValue);

            DoneGivingMana();
        }
        else {
            print("Already at max mana");
        }
    }

    protected virtual void DoneGivingMana() {

    }
}
