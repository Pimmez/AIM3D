using UnityEngine;

public class InteractWithObject : ManaCost {

    [SerializeField]
    private GameObject objectToActivate;

    protected override void Start()
    {
        base.Start();
        objectToActivate.SetActive(false);
    }

    protected override void EnoughMana()
    {
        base.EnoughMana();
        objectToActivate.SetActive(true);
    }
}
