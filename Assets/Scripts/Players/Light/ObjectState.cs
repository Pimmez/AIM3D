using UnityEngine;

public class ObjectState : MonoBehaviour {

    [SerializeField]
    private GameObject objToSwitch;

    private bool isActive;

    public void SwitchState() {
        isActive = !isActive;
        objToSwitch.SetActive(isActive);
    }

    public void SetState(bool state) {
        objToSwitch.SetActive(state);
        isActive = state;
    }

    public bool Active {
        get { return isActive; }
    }

    public GameObject Obj {
        set { objToSwitch = value; }
    }
}
