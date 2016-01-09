using UnityEngine;
using System.Collections;

public class ManaBar : MonoBehaviour {

    [SerializeField]
    private float maxMana = 50;

    [SerializeField]
    private int manaStartValue = 25;

    private float currentManaVal;

    private float oldBarSizeX;

    [SerializeField]
    private float smoothTime = 1f;

    private float velocity;

    private float startPosX;

    void Start() {
        startPosX = transform.position.x - transform.localScale.x;

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
        oldBarSizeX = currentManaVal;

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
            //assign the new scale
            transform.localScale = new Vector3(
                //smooth the size to the difference between old mana and current mana.
                oldBarSizeX = Mathf.SmoothDamp(oldBarSizeX, currentManaVal, ref velocity, smoothTime),
                transform.localScale.y,
                transform.localScale.z
            );

            //relocate the bar so that it only grows to the right
            //transform.position = new Vector3((transform.localScale.x * 5 ) + startPosX, transform.position.y, transform.position.z);
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