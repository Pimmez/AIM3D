using UnityEngine;

public class TurnObjectOn : ManaCost {

    [SerializeField]
    private GameObject objectToActivate;

    [SerializeField]
    private AudioSource turnOnSound;

    protected override void Awake()
    {
        base.Awake();
        objectToActivate.SetActive(false);
    }

    protected override void EnoughMana()
    {
        base.EnoughMana();
        objectToActivate.SetActive(true);
        turnOnSound.Play();
    }
}
