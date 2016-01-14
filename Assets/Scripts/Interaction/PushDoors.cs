using UnityEngine;
using System.Collections;

public class PushDoors : MonoBehaviour {

	[SerializeField]
	private float pushPower = 2f;

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;

		if (body == null || body.isKinematic) 
		{
			return;
		}

		if (hit.moveDirection.y < -0.3f) 
		{
			return;
		}

		Vector3 pushDirection = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);

		body.velocity = pushDirection * pushPower;
	}

}
