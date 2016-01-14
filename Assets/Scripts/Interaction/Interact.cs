using UnityEngine;

public class Interact : MonoBehaviour {

	[SerializeField]
	private int interactDistance = 5;

	// Update is called once per frame
	void Update () 
	{
		//if we recieve the interact input, look in front of us for a object with a tag
		if (Input.GetButtonDown("Interact")) 
		{
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit rayHit;
			if(Physics.Raycast(ray, out rayHit, interactDistance))
			{
                if (rayHit.collider.CompareTag("Interactive"))
				{
                    rayHit.transform.GetComponent<CheckVisibility>().PlayerLooksInMyDirection(transform,interactDistance);
                }
			}
        }
	}
}
