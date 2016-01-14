using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

    [SerializeField]
    private Camera cameraToPlaySound;

    [SerializeField]
    private float crouchingOffSet;

    private float standingOffSet;

    [SerializeField]
    private float offSet;

    private float shakeVelocity;

    private Vector3 moveVelocity;

    private bool shaking;

    private bool crouching;

    private Transform charTransform;

    void Start() {

        //the character is the parent
        charTransform = transform.parent;

        //save the startposition in the parent as the offset
        standingOffSet = offSet = transform.localPosition.y;


        transform.position = new Vector3(transform.position.x, charTransform.position.y + (offSet * charTransform.localScale.y), transform.position.z);
    }

    public void StartShake(float _shakeAmount, float _shakeTime) {
        //if we arent already shaking
        if (!shaking)
        {
            //we are shaking now
            shaking = true;

            StartCoroutine(UpDown(_shakeAmount, _shakeTime));

            //plays a random footstep from the randomsound list
            FootStep();
        }
    }

    public void StopShake() {
        //stop all coroutines in this scripts
        StopAllCoroutines();

        //set shaking to false, so we can start shaking again
        shaking = false;
    }

    private IEnumerator UpDown(float _shakeStrength, float _shakeTime)
    {
        float nextYPostion;

        //the minimal distance to complete the while loop
        float minDistance = _shakeStrength / 100;

        //i save the offSet because there is a chance we get problem if we change the offSet value while the coroutine is running
        //float tempoffset = offSet;

        //while we arent at right shake height, current height - (parent position Y - (offset * parent height*) - _shaking). 
        //when the result is smaller than minDistance we have reached the right y pos.
        //*to keep the offset always relative to the parent height in case when we are crouched
        while (Mathf.Abs(transform.position.y - ((charTransform.position.y + (offSet * charTransform.localScale.y)) - _shakeStrength)) > minDistance)
        {
            //smoothdamp to the next y pos.
            nextYPostion = Mathf.SmoothDamp(transform.position.y, (charTransform.position.y + (offSet * charTransform.localScale.y)) - _shakeStrength, ref shakeVelocity, _shakeTime);
            //our y position is next y pos.
            transform.position = new Vector3(transform.position.x, nextYPostion, transform.position.z);

            yield return new WaitForFixedUpdate();
        }

        //same as above. 
        //we use absolute because else its always smaller than minDistance
        while (Mathf.Abs(transform.position.y - (charTransform.position.y + (offSet * charTransform.localScale.y))) > minDistance)
        {
            nextYPostion = Mathf.SmoothDamp(transform.position.y, charTransform.position.y + (offSet * charTransform.localScale.y), ref shakeVelocity, _shakeTime);
            transform.position = new Vector3(transform.position.x, nextYPostion, transform.position.z);
            yield return new WaitForFixedUpdate();
        }

        //shaking has ended, and we can shake again.
        shaking = false;

        //plays a random footstep from the randomsound list
        FootStep();
    }

    protected virtual void FootStep() {
        //play a random footstep sound from the sound list
        if (cameraToPlaySound != null)
        {
            //check if we are walking
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                cameraToPlaySound.GetComponent<RandomSound>().PlayRandomSound();
            }
        }
    }

    public bool Shaking {
        get { return shaking; }
    }

    public void SwitchOffSet(bool _crouching)
    {
        if (_crouching) {
            offSet = crouchingOffSet;
        } else {
            offSet = standingOffSet;
        }
    }
}
