using UnityEngine;

public class TogglePauzed : MonoBehaviour {

    private GameSpeed gameSpeed;

    void Start() {
        gameSpeed = GameObject.FindGameObjectWithTag("Pauze").GetComponent<GameSpeed>();
        gameSpeed.SetGameSpeed(1);
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Cancel")) {
            GetComponent<ToggleGameobjectActive>().ToggleState();
            GetComponent<SetSelected>().SetNewSelected();
            gameSpeed.SwitchGameSpeed();
        }
	}
}
