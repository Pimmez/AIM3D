using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFogSetting : MonoBehaviour
{
    [SerializeField]
    float thisCamerafogStrength = 0.08f;

    float otherCameraFogStrength;

    void OnPreRender()
    {
        //save the previous fog strength
        otherCameraFogStrength = RenderSettings.fogDensity;

        //aply the new fog strength before we render the object
        RenderSettings.fogDensity = thisCamerafogStrength;
    }

    void OnPostRender()
    {
        //return the fogdensity to the previous fog strenght we saved earlier
        RenderSettings.fogDensity = otherCameraFogStrength;
    }
}