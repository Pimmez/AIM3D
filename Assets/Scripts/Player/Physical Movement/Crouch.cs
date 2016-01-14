using UnityEngine;
using System.Collections;

public class Crouch : MonoBehaviour {

    [SerializeField]
    private float resizeYTime = 0.2f;

    [SerializeField]
    private float resizeMinDifference = 0.01f;

    [SerializeField]
    private int upwardsSaveZoneLength = 2;

    private float resizeYVelocity;

    bool changingCrouch;

    public void ChangeCrouchState(float _ySize) {
        StartCoroutine(changeYSize(_ySize));
    }

    public bool CheckIfCanCrouch(bool _goCrouching)
    {
        bool returnVal = true;
        //if we are smaller then the _ySize, we are standing up and we need to check if there is something above is so we dont get stuck in it.
        //else we are crouching which is always possible
        if (!_goCrouching)
        {
            Vector3 rayEnd = transform.TransformDirection(Vector3.up);
            //check with a raycast if there is something above us.
            if (Physics.Raycast(transform.position, rayEnd, upwardsSaveZoneLength))
            {
                //there is something above us, so we cant stand up
                returnVal = false;
            }        
        }
        return returnVal;
    }

    private IEnumerator changeYSize(float yToMoveTo)
    {
        float nextYSize;

        //so we cant start the coroutine while we are in the coroutine
        changingCrouch = true;

        //resize the player to the right size
        while (Mathf.Abs(transform.localScale.y - yToMoveTo) > resizeMinDifference)
        {
            nextYSize = Mathf.SmoothDamp(transform.localScale.y, yToMoveTo, ref resizeYVelocity, resizeYTime);
            transform.localScale = new Vector3(transform.localScale.x, nextYSize, transform.localScale.z);
            yield return new WaitForFixedUpdate();
        }

        changingCrouch = false;
    }

    public bool GetChangingCrouch {
        get { return changingCrouch; }
    }
}
