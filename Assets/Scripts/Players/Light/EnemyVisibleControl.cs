using UnityEngine;
using System.Collections;

public class EnemyVisibleControl : LightControl {

    private ObjectVisibleState objVisibleState;

    protected override void Awake()
    {
        base.Awake();

        objVisibleState = GetComponent<ObjectVisibleState>();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            objVisibleState.AddObjectToMakeInvisible(enemy.GetComponent<Renderer>());
        }

        objVisibleState.SetState(false);
    }

    protected override void SwitchLight(bool _lightState)
    {
        base.SwitchLight(_lightState);
        objVisibleState.SwitchState();
    }
}
