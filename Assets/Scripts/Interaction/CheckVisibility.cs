using UnityEngine;
using System.Collections;

public class CheckVisibility : MonoBehaviour
{

    private float notVisibleTime;

    protected virtual void Awake()
    {
        NotVisible();
    }


    protected virtual void Visible()
    {

    }

    protected virtual void NotVisible()
    {

    }

    public void PlayerLooksInMyDirection(Transform _plr, int _newNotVisibleTime) {
        //reset the notVisibleTime, to the cooldown the player checks with raycast, plus 1 to be sure
        notVisibleTime = _newNotVisibleTime + 1;
        Visible();
    }

    IEnumerator WaitToSetNotVisible()
    {
        while (notVisibleTime > 0) {
            notVisibleTime--;
            yield return new WaitForFixedUpdate();
        }

        NotVisible();
    }
}
