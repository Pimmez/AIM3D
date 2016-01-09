using UnityEngine;

public class ActivateObject : ManaCost {

    [SerializeField]
    private GameObject objectToActivate;

    private bool startState;

    private void Start()
    {
        objectToActivate.SetActive(startState);
    }

    protected override void EnoughMana()
    {
        base.EnoughMana();
        objectToActivate.SetActive(!startState);
    }
}
