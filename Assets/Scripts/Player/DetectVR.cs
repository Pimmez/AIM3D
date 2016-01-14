using UnityEngine;
using UnityEngine.VR;

public class DetectVR : MonoBehaviour {

    protected bool VRPresent = false;

    void Awake() {
        //if VR is detected, VRPresent is true
        if (VRDevice.isPresent) {
            VRPresent = true;
        }
    }

    public bool VRIsPresent {
        get { return VRPresent; }
    }
}
