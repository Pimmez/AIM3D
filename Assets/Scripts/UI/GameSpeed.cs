using UnityEngine;

public class GameSpeed : MonoBehaviour {

    private bool gamePauzeState = true;

    public void SwitchGameSpeed()
    {
        gamePauzeState = !gamePauzeState;
        Time.timeScale = System.Convert.ToInt32(gamePauzeState);
    }

    public void SetGameSpeed(int _gameSpeed) {
        if (_gameSpeed == 0) gamePauzeState = false;
        else gamePauzeState = true;
        Time.timeScale = _gameSpeed;
    }
}
