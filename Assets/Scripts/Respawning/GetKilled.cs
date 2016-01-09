using UnityEngine;

public class GetKilled : CheckTriggerEnterTag {

    private Respawn respawn;

    void Awake() {
        respawn = GetComponent<Respawn>();
    }

    protected override void OnTriggerEnterWithTag()
    {
        base.OnTriggerEnterWithTag();
        //each enemy with the collisiontag
        foreach (GameObject enemiesRespawn in GameObject.FindGameObjectsWithTag(triggerTagName))
        {
            //make sure they lose sight of me
            enemiesRespawn.GetComponentInParent<FollowTarget>().LoseCurrentTarget();
            //respawn the enemy
            enemiesRespawn.GetComponentInParent<Respawn>().GoToSpawnPoint();
        }

        foreach (GameObject pickupHolders in GameObject.FindGameObjectsWithTag("ManaPillar")) {
            pickupHolders.GetComponent<ManaPickupSpawn>().SpawnManaPickup();
        }

        GameObject.Find("ManaBar").GetComponent<ManaBar>().ResetMana();

        //the player goes to my spawnpoint
        respawn.GoToSpawnPoint();
    }
}
