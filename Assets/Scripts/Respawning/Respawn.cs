using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

    [SerializeField]
    private Transform spawnPoint;

    public  void GoToSpawnPoint() {
        //reset my location to the spawnpoint
        transform.position = spawnPoint.position;

        //if i have a go to point smooth component, update my position in gotopoint smooth
        if(GetComponent<GoToPointSmooth>()) GetComponent<GoToPointSmooth>().UpdatePostion();
    }
}
