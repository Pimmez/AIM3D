using UnityEngine;

public class WaitForCheck : MonoBehaviour
{
    [SerializeField]
    protected float cooldown;

    virtual protected void Awake()
    {
        InvokeRepeating("Check", 0, cooldown);
    }

    virtual protected void Check()
    {

    }
}
