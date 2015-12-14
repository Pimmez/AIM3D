using UnityEngine;

public class WaitForCheck : MonoBehaviour
{
    [SerializeField]
    protected float checkCooldown;

    virtual protected void Awake()
    {
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
