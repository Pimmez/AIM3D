using UnityEngine;
using System.Collections;

public class SoundTimer : MonoBehaviour {

    [SerializeField]
    private GameObject cameraObj;

    RandomSound randomSound;

    private bool counting;

	// Use this for initialization
	void Start () {
        randomSound = cameraObj.GetComponent<RandomSound>();
	}

    public void StartWaitForTime(int _time) {
        // if we arent already counting
        if(!counting)StartCoroutine(WaitForTime(_time));
    }

    private IEnumerator WaitForTime(int _timeToWait)
    {
        counting = true;
        int time = _timeToWait;
        while (time > 0) {
            time--;
            yield return new WaitForFixedUpdate();
        }
        //when the time is over, check if we are still moving, if true, play a random sound
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.3f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.3f) randomSound.PlayRandomSound();
        counting = false;
    }
}
