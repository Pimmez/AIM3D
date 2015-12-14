using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour
{
    [SerializeField]
    private GameObject lightObj;

    [SerializeField]
    private float changeToFlicker;

    [SerializeField]
    private int maxFlickerTimes;

    [SerializeField]
    private int maxTimeBetweenFlicker;

    [SerializeField]
    private float maxRange;

    [SerializeField]
    private float minRange;

    [SerializeField]
    private float rangeChange;

    private ObjectActiveState objActiveState;

    private Noise noise;

    private bool canSwitch = true;

    [SerializeField]
    private int lightSwitchSoundStrength = 13;

    // Use this for initialization
    protected virtual void Awake()
    {
        if (lightObj == null) print("Assign Lightobject!");
        //the script that manages active state of the light.
        objActiveState = GetComponent<ObjectActiveState>();
        objActiveState.AddObjectToMakeInactive(lightObj);

        noise = GetComponentInParent<Noise>();
    }

    // Update is called once per frame
    void Update()
    {
        //if we can switch the light and we pressed the button
        if (Input.GetButtonDown("Light") && canSwitch)
        {
            //set the range to min range, if the obj is not active yet, we change it to max range.
            float range = minRange;
            if (!objActiveState.Active) range = maxRange;

            //the lights are being activated or deactivated in this coroutine.
            StartCoroutine(ChangeLightRange(range));

            //make some noise for the enemy to hear
            noise.NoiseArea(lightSwitchSoundStrength);
        }
        else if (objActiveState.Active)//when the light is on (active) it has a chance to flicker.
        {
            //the chance to flicker is dependend on the amount of mana (the more mana the smaller the chance).
            if (Random.Range(0, 0.99f) < (changeToFlicker)) StartCoroutine(Flicker());
        }
    }

    private IEnumerator Flicker()
    {
        //set canSwitch on false so that the player cant turn the camera off during flikkering.
        canSwitch = false;

        int timesToFlicker = Random.Range(1, maxFlickerTimes);
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

    private IEnumerator ChangeLightRange(float _targetRange)
    {
        Light plrLight = lightObj.GetComponent<Light>();

        //if the targetRange is the same as maxrange, it means the light is activating and we need to activate it before the rangeChange.
        if (_targetRange == maxRange)
        {
            plrLight.range = minRange;
            SwitchLight(true);
        }

        //change the range gradually to the target range
        while ((int)plrLight.range != _targetRange)
        {
            plrLight.range = Mathf.MoveTowards(plrLight.range, _targetRange, rangeChange);
            yield return new WaitForFixedUpdate();
        }

        //if the targetrange is the same as min range, then we know the light is being deactivated and we need to change it after we decremented the range.
        if (_targetRange == minRange) SwitchLight(false);
    }

    protected virtual void SwitchLight(bool _lightState) {
        objActiveState.SetState(_lightState);
    }
}
