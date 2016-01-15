using UnityEngine;
using System.Collections;

public class InteractiveDoors : MonoBehaviour {

	[SerializeField]
	private float interactDistance = 5f;

	// Update is called once per frame
	void Update () 
	{
		//on mouse click, you open the doors.
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit rayHit;
			if(Physics.Raycast(ray, out rayHit, interactDistance))
			{
				if(rayHit.collider.CompareTag("Door"))
				{
					rayHit.collider.transform.parent.GetComponent<Doors>().DoorAngle();
				}
			}
		}
	}
}
