using UnityEngine;
using System.Collections;

public class ManaBar : MonoBehaviour {

    [SerializeField]
    private float maxMana = 50;

    [SerializeField]
    private int manaStartValue = 25;

    [SerializeField]
    private float currentManaVal;

    private float oldBarSizeX;

    private float barSizeX = 0;

    [SerializeField]
    private float smoothTime = 1f;

    private float velocity;

    void Start() {
        ChangeMana(manaStartValue);
    }

	public bool UseMana(float _manaCost) 
	{
		//If The cost of the object is lower then the current mana we have, only then you can go through.
		if (currentManaVal >= _manaCost)
		{
            ChangeMana(-_manaCost);

			return true;
		}
		else
		{	
			//When there is not enough mana.
			print ("No Mana Available");
			return false;
		}
	}

    public void ChangeMana(float change) {

        //we save the old mana value
        oldBarSizeX = currentManaVal * 0.3f;

        //if the new value will become wont become too low
        if (currentManaVal + change > 0)
        {
            //the new the value wont become too high
            if (currentManaVal + change < maxMana)
            {
                //change the current value
                currentManaVal += change;
            } // else it will be the same as maxMana
            else {
                currentManaVal = maxMana;
            }
        } // else it will be 0
        else {
            currentManaVal = 0;
        }
    }

    private void SetMana(float newManaValue) {
        //we save the old mana value
        oldBarSizeX = currentManaVal;

        currentManaVal = newManaValue;
    }

    public void ResetMana() {
        SetMana(manaStartValue);
    }

	void FixedUpdate()
	{
        if (oldBarSizeX != currentManaVal)
        {
            //float oldBarX = oldBarSizeX / maxMana * 4;
            //float newBarX = currentManaVal / maxMana * 4;

            //assign the new scale
            transform.localScale = new Vector3(
                //smooth the size to the difference between old mana and current mana.
                oldBarSizeX = Mathf.SmoothDamp(oldBarSizeX, currentManaVal, ref velocity, smoothTime) * 0.3f,
                transform.localScale.y,
                transform.localScale.z
            );
            print(oldBarSizeX);
        }
    }

    public void StartManaDraining(float _costPerFrame) {
        StartCoroutine(DrainMana(_costPerFrame));
    }

    public void StopManaDraining()
    {
        StopCoroutine("DrainMana");
    }

    private IEnumerator DrainMana(float _cost)
    {
        ChangeMana(-_cost);
        yield return new WaitForFixedUpdate();
    }

    public float CurrentManaVal {
        get { return currentManaVal; }
    }

    public float MaxMana
    {
        get { return maxMana; }
    }

}