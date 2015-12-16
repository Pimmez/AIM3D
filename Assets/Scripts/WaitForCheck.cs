using UnityEngine;

public class WaitForCheck : MonoBehaviour
{
    [SerializeField]
    protected float checkCooldown;

    virtual protected void Awake()
    {
        //call the function repeatetly
        InvokeRepeating("Check", Random.Range(0, 0.99f), checkCooldown);
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
