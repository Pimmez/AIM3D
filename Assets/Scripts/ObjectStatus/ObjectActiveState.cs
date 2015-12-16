using UnityEngine;
using System.Collections.Generic;

public class ObjectActiveState : MonoBehaviour {

    [SerializeField]
    private List<GameObject> objsToMakeInactive;

    private bool isActive;

    public void SwitchState() {
        isActive = !isActive;
        foreach (GameObject obj in objsToMakeInactive) {
            obj.SetActive(isActive);
        }
    }

    public void SetState(bool _state) {
        foreach (GameObject obj in objsToMakeInactive)
        {
            obj.SetActive(_state);
        }
        isActive = _state;
    }

    public bool Active {
        get { return isActive; }
    }

    public void AddObjectToMakeInactive(GameObject _objToAdd) {
        objsToMakeInactive.Add(_objToAdd);
    }
}
