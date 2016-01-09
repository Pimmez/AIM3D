using UnityEngine;

public class ToggleGameobjectActive : MonoBehaviour {

    [SerializeField]
    private GameObject objectToToggle;

    public void ToggleState() {
        //if the object is active, set it inactive
        if (objectToToggle.activeSelf) objectToToggle.SetActive(false);
        //or if the object is inactive, set it active
        else objectToToggle.SetActive(true);
    }

    public void SetState(bool _active) {
        objectToToggle.SetActive(_active);
    }
}
