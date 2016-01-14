using UnityEngine;

public class HideSpriteOnAwake : MonoBehaviour
{

    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
}