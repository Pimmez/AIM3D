using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    [SerializeField]
    private float shakeSpeed;

    private float shakeVelocity;

    public IEnumerator Shake(float _shakeStrenght)
    {
        float oldYPos = transform.position.y;
        float nextYPostion;
        while (transform.position.y != transform.position.y - _shakeStrenght)
        {
            nextYPostion = Mathf.SmoothDamp(transform.position.y, transform.position.y - _shakeStrenght, ref shakeVelocity, shakeSpeed);
            transform.position = new Vector3(transform.position.x, nextYPostion, transform.position.z);
            yield return new WaitForFixedUpdate();
        }
        while (transform.position.y != oldYPos)
        {
            nextYPostion = Mathf.SmoothDamp(transform.position.y, oldYPos, ref shakeVelocity, shakeSpeed);
            transform.position = new Vector3(transform.position.x, nextYPostion, transform.position.z);
            yield return new WaitForFixedUpdate();
        }
    }
}
