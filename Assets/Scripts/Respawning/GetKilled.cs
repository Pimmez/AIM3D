using UnityEngine;
using System.Collections;

public class GetKilled : MonoBehaviour {

    [SerializeField]
    private string collisionTagName;

    private Respawn respawn;

    void Awake() {
        respawn = GetComponent<Respawn>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag(collisionTagName))
        {
            //each enemy with the collisiontag
            foreach (GameObject enemiesRespawn in GameObject.FindGameObjectsWithTag(collisionTagName))
            {
                //make sure they lose sight of me
                enemiesRespawn.GetComponentInParent<FollowTarget>().LoseCurrentTarget();
                //respawn the enemy
                enemiesRespawn.GetComponentInParent<Respawn>().GoToSpawnPoint();
            }
            //i go to my spawnpoint
            respawn.GoToSpawnPoint();
        }
    }
}
