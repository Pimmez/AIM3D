using UnityEngine;
using System.Collections.Generic;

public class ObjectActiveState : MonoBehaviour {

    [SerializeField]
    private List<GameObject> objsToSwitchState;

    private bool isActive;

    public void SwitchState() {
        foreach (GameObject obj in objsToSwitchState) {
            obj.SetActive(!obj.activeSelf);
            isActive = !obj.activeSelf;
        }
    }

    public void SetState(bool _state) {
        foreach (GameObject obj in objsToSwitchState)
        {
            obj.SetActive(_state);
        }
        isActive = _state;
    }

    public bool Active {
        get { return isActive; }
    }

    public void AddObjectToMakeInactive(GameObject _objToAdd) {
        objsToSwitchState.Add(_objToAdd);
    }
}
