using UnityEngine;

public class TogglePauzeUI : MonoBehaviour {

    private ObjectActiveState toggleActive;

    private SetSelected setSelected;

    private GameSpeed gameSpeed;

    void Awake() {
        //assing all variables
        toggleActive = GetComponent<ObjectActiveState>();
        setSelected = GetComponent<SetSelected>();
        gameSpeed = GetComponent<GameSpeed>();

        gameSpeed.SetGameSpeed(1);
    }

    public void TogglePauze()
    {
        //switch the pauzemenu
        toggleActive.SwitchState();

        //select the new selected button
        setSelected.SetNewSelected();

        //switch the game speed
        gameSpeed.SwitchGameSpeed();
    }

}
