using UnityEngine;
using System.Collections;

public class GoDown : MonoBehaviour {

    [SerializeField]
    private float goDownAmount = 0.5f;

    [SerializeField]
    private float moveTime = 0.2f;

    private float startYPos;

    private bool going;

    void Start() {
        startYPos = transform.localPosition.y;
    }

    public void StartMoving(bool _goUp)
    {
        float endPos = startYPos - goDownAmount;
        if (_goUp) {
            endPos = startYPos;
        }
        StartCoroutine(GoToYPos(endPos));
    }

    private IEnumerator GoToYPos(float _endPos)
    {
        float nextYPostion;

        //the minimal distance to complete the while loop
        float minDistance = goDownAmount / 100;

        float shakeVelocity = new float();

        going = true;

        while (Mathf.Abs(transform.localPosition.y - _endPos) > minDistance)
        {
            //smoothdamp to the next y pos.
            nextYPostion = Mathf.SmoothDamp(transform.localPosition.y, _endPos, ref shakeVelocity, moveTime);
            //our y position is next y pos.
            transform.localPosition = new Vector3(transform.localPosition.x, nextYPostion, transform.localPosition.z);

            print("going down");
            yield return new WaitForFixedUpdate();
        }

        going = false;
    }

    public bool Going {
        get { return going; }
    }
}
