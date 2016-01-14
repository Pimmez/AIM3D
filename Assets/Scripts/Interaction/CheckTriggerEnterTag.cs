using UnityEngine;

public class CheckTriggerEnterTag : MonoBehaviour
{
    [SerializeField]
    protected string triggerTagName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTagName))
        {
            OnTriggerEnterWithTag();
        }
    }

    protected virtual void OnTriggerEnterWithTag()
    {

    }
}

