using UnityEngine;

public class ManaPickupSpawn : MonoBehaviour {

    [SerializeField]
    private GameObject manaPickup;

    [SerializeField]
    private Transform spawnPoint;

    private bool hasPickup;

    void Start() {
        SpawnManaPickup();
    }

    public void SpawnManaPickup() {
        if (!hasPickup)
        {
            GameObject pickup = Instantiate(manaPickup, spawnPoint.position, spawnPoint.rotation) as GameObject;
            pickup.transform.parent = transform.parent;
            hasPickup = true;
        }
    }

    public void GetPickedUp() {
        hasPickup = false;
    }
}
