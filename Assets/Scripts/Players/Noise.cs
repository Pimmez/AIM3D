using UnityEngine;

public class Noise : MonoBehaviour {

    [SerializeField]
    protected LayerMask checkLayer;

    public void NoiseArea(float _soundStrength)
    {
        foreach (Collider target in Physics.OverlapSphere(transform.position, _soundStrength, checkLayer))
        {
            target.GetComponent<CheckForTarget>().SeeTarget(this.gameObject.transform);
        }
    }
}
