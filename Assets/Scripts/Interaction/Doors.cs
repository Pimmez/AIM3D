using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {

	[SerializeField]
	private bool open = false;

	[SerializeField]
	private float doorOpenAngle = 90f;
	
	[SerializeField]
	private float doorCloseAngle = 0;
	
	[SerializeField]
	private float smooth = 2f;

	public void DoorAngle()
	{
		open = !open;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Als de deur open is dan gaat die naar de toegewezen angle.
		if (open) 
		{
			Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth*Time.deltaTime);
		} 
		//Als de deur dicht is dan gaat die naar de toegewezen angle.
		else
		{
			Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth*Time.deltaTime);
		}
	}
}
