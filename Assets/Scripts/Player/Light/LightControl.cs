using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour
{
    [SerializeField]
    private GameObject lightObj;

    [SerializeField]
    private float manaCost = 0.5f;

    [SerializeField]
    private float changeToFlicker = 0.003f;

    [SerializeField]
    private int maxFlickerTimes = 3;

    [SerializeField]
    private int maxTimeBetweenFlicker = 23;

    [SerializeField]
    private float maxRange = 100;

    [SerializeField]
    private float minRange = 1;

    private float range;

    [SerializeField]
    private float rangeChangeSpeed = 2f;

    private bool reduceRangeToManaVal;

    private ObjectActiveState objActiveState;

    private ManaBar manaBar;

    private Noise noise;

    private bool canSwitch = true;

    [SerializeField]
    private int lightSwitchSoundStrength = 6;

    [SerializeField]
    private GameObject rightHand;

    private Animator anim;

    [SerializeField]
    private AudioSource lightOnSound;

    [SerializeField]
    private AudioSource lightOffSound;

    // Use this for initialization
    protected virtual void Start()
    {
        //the script that manages active state of the light.
        objActiveState = GetComponent<ObjectActiveState>();
        objActiveState.AddObjectToMakeInactive(lightObj);

        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<ManaBar>();

        noise = GetComponentInParent<Noise>();

        anim = rightHand.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if we can switch the light and we pressed the button
        if (Input.GetButtonDown("Light") && canSwitch && manaBar.CurrentManaVal > 0)
        {
            print("hello");

            //anim.Play("Spell");
            anim.SetTrigger("Interact");
            //anim.SetBool("Interact", false);


            //set the range to min range, if the obj is not active yet, we change it to max range.
            range = minRange;
            if (!objActiveState.Active) range = maxRange;

            StartRangeChange(range);

            //make some noise for the enemy to hear
            noise.NoiseArea(lightSwitchSoundStrength);
        }
        else if (objActiveState.Active)//when the light is on (active) it has a chance to flicker.
        {

            if (manaBar.CurrentManaVal > 0)
            {
                //if the change light range coroutine isnt already busy
                if (canSwitch)
                {
                    //decrement mana
                    manaBar.ChangeMana(-manaCost * Time.deltaTime);

                    if (reduceRangeToManaVal)
                    {
                        StartRangeChange(
                             //range * maxmana / currentmana, diveded by 2, plus range devided by 2.
                             //make sure range gets max 50% lower then the original range
                             range / (manaBar.MaxMana / manaBar.CurrentManaVal)
                        );
                    }

                    //the chance to flicker is dependend on the amount of mana (the more mana the smaller the chance).
                    else if (Random.Range(0, 0.99f) <
                        //make sure changeToFlicker gets max 50% lower then the original changeToFlicker
                        (((changeToFlicker * (manaBar.MaxMana / manaBar.CurrentManaVal) / 3) + changeToFlicker / 1.5f) * Time.deltaTime))
                    {
                        StartFlicker();

                        //every time we flicker, when flickering is done, we lower the range of the light to the current mana.
                        reduceRangeToManaVal = true;
                    }
                }
            }
            else {
                //the lights are being activated or deactivated in this coroutine.
                StartRangeChange(minRange);
            }
        }
    }

    private void StartFlicker() {
        if (canSwitch)
        {
            canSwitch = false;
            StartCoroutine(Flicker());
        }
    }

    private void StartRangeChange(float _targetRange) {
        if (canSwitch) {
            canSwitch = false;
            StartCoroutine(ChangeLightRange(_targetRange, rangeChangeSpeed));
        }
    }

    private IEnumerator Flicker()
    {
        //set canSwitch on false so that the player cant turn the camera off during flikkering.
        canSwitch = false;

        int timesToFlicker = Random.Range(2, maxFlickerTimes);
        for (int i = 0; i < timesToFlicker; i++)
        {

            //switch the light twice, first off then on.
            //also adds a very short random pause between switching
            for (int s = 0; s < 2; s++)
            {
                //switch light
                objActiveState.SwitchState();

                //also adds a very short random pause between switching
                //except if this is the very last flicker and the light remains on anyway, there wont be any need to add a pause then.
                if (i + 1 < timesToFlicker)
                {
                    float maxTime = Random.Range(3, maxTimeBetweenFlicker);
                    float time = 0;

                    while (time <= maxTime)
                    {
                        time++;
                        yield return new WaitForFixedUpdate();
                    }
                }
            }
        }
        //the player gets control over the light again.
        canSwitch = true;
    }

    private IEnumerator ChangeLightRange(float _targetRange, float _rangeChangeSpeed)
    {
        Light plrLight = lightObj.GetComponent<Light>();

        //if the targetRange is the same as maxrange, it means the light is activating and we need to activate it before the rangeChange.
        if (_targetRange == maxRange)
        {
            plrLight.range = minRange;
            SwitchLight(true);
            lightOnSound.Play();
        }
        else {
            lightOffSound.Play();
        }

        //change the range gradually to the target range
        while (plrLight.range != _targetRange)
        {
            plrLight.range = Mathf.MoveTowards(plrLight.range, _targetRange, _rangeChangeSpeed);
            yield return new WaitForFixedUpdate();
        }

        //if the targetrange is the same as min range, then we know the light is being deactivated and we need to change it after we decremented the range.
        if (_targetRange == minRange)
        {
            SwitchLight(false);
        }

        canSwitch = true;

        //we prevent reducing the range to mana again until we flicker again.
        reduceRangeToManaVal = false;
    }

    protected virtual void SwitchLight(bool _lightState) {
        objActiveState.SetState(_lightState);
    }
}
