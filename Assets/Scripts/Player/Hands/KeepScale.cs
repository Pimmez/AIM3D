using UnityEngine;
using System.Collections;

public class KeepScale : MonoBehaviour {

    public void StartKeepHeightScale(float _newSizeY)
    {
        StartCoroutine(KeepYSize(_newSizeY));
    }

    private IEnumerator KeepYSize(float _newSizeY)
    {
        //while we are scaling the parent character/player
        while (Mathf.Abs(transform.parent.localScale.y - _newSizeY) > 0.01f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / transform.lossyScale.y, transform.localScale.z);
            yield return new WaitForFixedUpdate();
        }
    }
}
