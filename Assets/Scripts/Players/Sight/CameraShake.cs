using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    [SerializeField]
    private float cameraOffset;

    private float shakeVelocity;

    private bool shaking;

    private Transform charTransform;

    void Awake() {
        charTransform = transform.parent;
        transform.position = new Vector3(transform.position.x, charTransform.position.y + cameraOffset, transform.position.z);
    }

    public void startCamShake(float _shakeAmount, float shakeSpeed) {
        if (!shaking)
        {
            shaking = true;
            StartCoroutine(CamShake(_shakeAmount, shakeSpeed));
        }
    }

    private IEnumerator CamShake(float _shakeStrength, float _shakeSpeed)
    {
        float nextYPostion;
        float minDistance = _shakeStrength / 100;
        while (transform.position.y - ((charTransform.position.y + cameraOffset) - _shakeStrength) > minDistance)
        {
            nextYPostion = Mathf.SmoothDamp(transform.position.y, (charTransform.position.y + cameraOffset) - _shakeStrength, ref shakeVelocity, _shakeSpeed);
            transform.position = new Vector3(transform.position.x, nextYPostion, transform.position.z);
            yield return new WaitForFixedUpdate();
        }
        while (Mathf.Abs(transform.position.y - (charTransform.position.y + cameraOffset)) > minDistance)
        {
            nextYPostion = Mathf.SmoothDamp(transform.position.y, charTransform.position.y + cameraOffset, ref shakeVelocity, _shakeSpeed);
            transform.position = new Vector3(transform.position.x, nextYPostion, transform.position.z);
            yield return new WaitForFixedUpdate();
        }
        shaking = false;
    }
}
