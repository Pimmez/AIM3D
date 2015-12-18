using UnityEngine;
using System.Collections;

public class Crouch : MonoBehaviour {

    [SerializeField]
    private float resizeYTime = 0.2f;

    [SerializeField]
    private float resizeMinDifference = 0.01f;

    private float resizeYVelocity;

    bool changingCrouch;

    public void ChangeCrouchState(float _ySize) {
        StartCoroutine(changeYSize(_ySize));
    }

    private IEnumerator changeYSize(float yToMoveTo)
    {
        float nextYSize;

        changingCrouch = true;

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
