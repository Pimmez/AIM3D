using UnityEngine;
using System.Collections.Generic;

public class ObjectVisibleState : MonoBehaviour
{

    [SerializeField]
    private List<Renderer> objsToMakeInvisible;

    private bool isVisible;

    public void SwitchState()
    {
        isVisible = !isVisible;
        foreach (Renderer obj in objsToMakeInvisible)
        {
            obj.enabled = isVisible;
        }
    }

    public void SetState(bool _state)
    {
        foreach (Renderer obj in objsToMakeInvisible)
        {
            obj.enabled = isVisible;
        }
        isVisible = _state;
    }

    public bool Active
    {
        get { return isVisible; }
    }

    public void AddObjectToMakeInvisible(Renderer _objToAdd)
    {
        objsToMakeInvisible.Add(_objToAdd);
    }
}
