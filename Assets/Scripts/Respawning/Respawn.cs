using UnityEngine;

public class Respawn : MonoBehaviour {

    [SerializeField]
    private Transform spawnPoint;

    public  void GoToSpawnPoint() {
        //if there is a spawnpoint avaible
        if (spawnPoint != null)
        {
            //reset my location to the spawnpoint
            transform.position = spawnPoint.position;

            //if i have a go to point smooth component, update my position in gotopoint smooth
            if (GetComponent<GoToPointSmooth>()) GetComponent<GoToPointSmooth>().UpdatePostion();
        }
        //destroy the object if not
        else {
            //GetComponent<CheckForTarget>().RemoveFromSoundList(GetComponent<CheckForTarget>().Target);

            GetComponent<CheckForTarget>().DestroyMyself();

            Destroy(gameObject);
        }
    }
}
