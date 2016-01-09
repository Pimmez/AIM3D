using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

    private float offset;

    private float shakeVelocity;

    private Vector3 moveVelocity;

    private bool shaking;

    private bool crouching;

    private Transform charTransform;

    void Start() {

        //the character is the parent
        charTransform = transform.parent;

        //save the startposition in the parent as the offset
        offset = transform.localPosition.y;

        transform.position = new Vector3(transform.position.x, charTransform.position.y + (offset * charTransform.localScale.y), transform.position.z);
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

    private IEnumerator UpDown(float _shakeStrength, float _shakeTime)
    {
        float nextYPostion;

        //the minimal distance to complete the while loop
        float minDistance = _shakeStrength / 100;

        //while we arent at right shake height, current height - (parent position Y - (offset * parent height*) - _shaking). 
        //when the result is smaller than minDistance we have reached the right y pos.
        //*to keep the offset always relative to the parent height in case when we are crouched
        while (Mathf.Abs(transform.position.y - ((charTransform.position.y + (offset * charTransform.localScale.y)) - _shakeStrength)) > minDistance)
        {
            //smoothdamp to the next y pos.
            nextYPostion = Mathf.SmoothDamp(transform.position.y, (charTransform.position.y + (offset * charTransform.localScale.y)) - _shakeStrength, ref shakeVelocity, _shakeTime);
            //our y position is next y pos.
            transform.position = new Vector3(transform.position.x, nextYPostion, transform.position.z);
            yield return new WaitForFixedUpdate();
        }

        //same as above. 
        //we absolute because else its always smaller than minDistance
        while (Mathf.Abs(transform.position.y - (charTransform.position.y + (offset * charTransform.localScale.y))) > minDistance)
        {
            nextYPostion = Mathf.SmoothDamp(transform.position.y, charTransform.position.y + (offset * charTransform.localScale.y), ref shakeVelocity, _shakeTime);
            transform.position = new Vector3(transform.position.x, nextYPostion, transform.position.z);
            yield return new WaitForFixedUpdate();
        }

        //shaking has ended, and we can shake again.
        shaking = false;

        print("end of shake");

        //plays a random footstep from the randomsound list
        FootStep();
    }

    protected virtual void FootStep() {
        print("try to play");
        //play a random footstep sound from the sound list
        if (GetComponent<RandomSound>())
        {
            //check if we are walking
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f) GetComponent<RandomSound>().PlayRandomSound();
            print(Input.GetAxis("Horizontal"));
        }
    }

    public bool Shaking {
        get { return shaking; }
    }
}
