using UnityEngine;

public class SetSelected : MonoBehaviour {

    [SerializeField]
    private GameObject objToSelect;

    public void SetNewSelected() {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(objToSelect);
    }
}
