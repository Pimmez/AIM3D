using UnityEngine;
using System.Collections.Generic;

public class Noise : MonoBehaviour {

    //enemies in this list are add and removed by the enemy itself when the player enters their trigger.
    [HideInInspector]
    public List<GameObject> enemiesThatCanHearMe;

    public void NoiseArea(float _soundStrength)
    {
        foreach (GameObject enemy in enemiesThatCanHearMe) {
            // if the distance between the player and the enemy is lower then the strenght of the sound, the enemy heard the player.
            if (Vector3.Distance(transform.position, enemy.transform.position) < _soundStrength) {
                enemy.GetComponent<FollowTarget>().SeeTarget(gameObject.transform);
            }
        }
    }
}
