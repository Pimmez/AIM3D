 using UnityEngine;
 using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class HideLightsFromCamera : MonoBehaviour
{
    [SerializeField]
    string ObjWithLightToHideTag;

    List<Light> lightsToHide;

    void Start() {
        lightsToHide = new List<Light>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(ObjWithLightToHideTag)) {
            foreach (Light light in obj.GetComponentsInChildren<Light>()) {
                lightsToHide.Add(light);
            }
        }
    }

    void OnPreCull()
    {
        //disable all lights before we render the screen.
        foreach (Light light in lightsToHide)
        {
            light.enabled = false;
        }
    }

    void OnPostRender()
    {
        //then enable it again when we have rendered it
        foreach (Light light in lightsToHide)
        {
            light.enabled = true;
        }
    }

    public void RemoveFormLightsToHide(Light _lightToRemove) {
        lightsToHide.Remove(_lightToRemove);
    }
}