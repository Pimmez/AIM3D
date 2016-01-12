using UnityEngine;

public class GameSpeed : MonoBehaviour {

    private bool gamePauzeState = true;

    public void SwitchGameSpeed()
    {
        gamePauzeState = !gamePauzeState;

        //convert boolean to int, and use it as gamespeed
        Time.timeScale = System.Convert.ToInt32(gamePauzeState);
    }

    public void SetGameSpeed(int _gameSpeed) {
        //check if gamespeed is set to zero, if it is, also set gamePauzeState to false (Zero)
        if (_gameSpeed == 0) gamePauzeState = false;
        else gamePauzeState = true;
        Time.timeScale = _gameSpeed;
    }
}
