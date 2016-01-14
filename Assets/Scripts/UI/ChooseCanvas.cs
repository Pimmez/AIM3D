using UnityEngine;

public class ChooseCanvas : MonoBehaviour
{
    [SerializeField]
    protected GameObject VRCanvas;

    [SerializeField]
    protected GameObject noVRCanvas;

    private void Start()
    {
        print("remeber to set back");
        VRPresent();
        //at the start of the game, we choose what kind interface we activate depeding on the presence of VR
        //if (GetComponent<DetectVR>().VRIsPresent) VRPresent();
        //else VRNotPresent();
    }

    protected virtual void VRPresent() {
        VRCanvas.SetActive(true);
        noVRCanvas.SetActive(false);
    }

    protected virtual void VRNotPresent() {
        VRCanvas.SetActive(false);
        noVRCanvas.SetActive(true);
    }
}