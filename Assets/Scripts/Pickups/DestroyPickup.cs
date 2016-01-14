using UnityEngine;

public class DestroyPickup : GivePlayerMana {
    
    protected override void DoneGivingMana()
    {
        base.DoneGivingMana();

        //remove the light in the pickup from camera masters hidelighst list
        //GameObject.Find("Camera Master").GetComponent<HideLightsFromCamera>().RemoveFormLightsToHide(GetComponentInChildren<Light>());

        //let the pickupSpawner know we are picked up
        foreach (Transform sibling in transform.parent)
        {
            if (sibling.GetComponent<ManaPickupSpawn>() != null)
            {
                sibling.GetComponent<ManaPickupSpawn>().GetPickedUp();
            }
        }

        //destroy myself
        Destroy(gameObject);
    }
}
