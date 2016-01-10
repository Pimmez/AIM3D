using UnityEngine;
using System.Collections;

public class InteractiveDoors : MonoBehaviour {

	[SerializeField]
	private float interactDistance = 5f;

	// Update is called once per frame
	void Update () 
	{
		//on mouse click, you open the doors in a radius of 5f.
		if (Input.GetKeyDown (KeyCode.Mouse0)) 
		{
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, interactDistance))
			{
				if(hit.collider.CompareTag("Door"))
				{
					hit.collider.transform.parent.GetComponent<Doors>().ChangeDoorState();
				}
			}
		}
	}
}
