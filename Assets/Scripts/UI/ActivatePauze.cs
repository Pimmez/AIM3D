using UnityEngine;

public class ActivatePauze : MonoBehaviour {

    [SerializeField]
    private GameObject Canvas;

    private TogglePauzeUI pauzeUI;

    void Start()
    {
        pauzeUI = Canvas.transform.Find("Pauze").GetComponent<TogglePauzeUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauzeUI.TogglePauze();
        }
    }
}
