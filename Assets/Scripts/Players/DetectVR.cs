using UnityEngine;
using UnityEngine.VR;

public class DetectVR : MonoBehaviour {

    bool oculusPresent = false;

    void Start() {
        if (VRDevice.isPresent) {
            oculusPresent = true;
        }
    }

    public bool OculusPresent {
        get { return oculusPresent; }
    }
}
