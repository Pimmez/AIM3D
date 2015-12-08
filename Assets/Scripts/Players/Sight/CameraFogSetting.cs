using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFogSetting : MonoBehaviour
{
    [SerializeField]
    bool enableFog = true;

    bool previousFogState;

    [SerializeField]
    float fogStrength = 0.15f;

    float previousFogStrength;

    void OnPreRender()
    {
        previousFogState = RenderSettings.fog;

        previousFogStrength = RenderSettings.fogDensity;

        RenderSettings.fog = enableFog;
        RenderSettings.fogDensity = fogStrength;
    }

    void OnPostRender()
    {
        RenderSettings.fog = previousFogState;
        RenderSettings.fogDensity = previousFogStrength;
    }

}