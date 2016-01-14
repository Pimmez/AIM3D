using UnityEngine;

public class SpawnGhost : CheckTriggerEnterTag {

    [SerializeField]
    private Transform ghostSpawnPoint;

    [SerializeField]
    private GameObject ghostToSpawn;

    [SerializeField]
    private Transform targetForEnemy;

    protected override void OnTriggerEnterWithTag()
    {
        base.OnTriggerEnterWithTag();

        //spawn the enemy
        GameObject ghost = Instantiate(ghostToSpawn, ghostSpawnPoint.position, ghostSpawnPoint.rotation) as GameObject;

        //the enemy sees the player immediatly
        ghost.GetComponent<CheckForTarget>().SeeTarget(targetForEnemy);

        //the enemy is not patrolling
        ghost.GetComponent<GoToPointSmooth>().setPatrolling(false);

        //remove the light from 
        foreach (HideLightsFromCamera light in targetForEnemy.GetComponentsInChildren<HideLightsFromCamera>()) {
            light.RemoveFormLightsToHide(GetComponentInChildren<Light>());
        }

        //destroy this trap
        Destroy(gameObject);
    }
}
