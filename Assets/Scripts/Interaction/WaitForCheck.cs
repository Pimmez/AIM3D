using UnityEngine;

public class WaitForCheck : MonoBehaviour
{
    [SerializeField]
    protected float checkCooldown = 0.5f;

    virtual protected void Start()
    {
        //call the function repeatetly
        if(checkCooldown != 0) InvokeRepeating("Check", Random.Range(0, 0.99f), checkCooldown);
    }

    virtual protected void Check()
    {

    }

    protected void startChecking()
    {
        InvokeRepeating("Check", 0, checkCooldown);
    }

    protected void stopChecking() {
        CancelInvoke("Check");
    }
}
