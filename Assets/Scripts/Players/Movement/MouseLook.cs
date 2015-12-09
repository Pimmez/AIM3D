using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

    [SerializeField]
    private float lookSensitivity = 5;

    [SerializeField]
    private float lookSmoothDamp = 0.1f;

    private float xRotation;

    private float yRotation;

    private float currentXRotation;

    private float currentYRotation;

    private float xRotationVelocity;

    private float yRotationVelocity;

    private Transform character;

    // Use this for initialization
    void Awake () {
        character = transform.parent;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVelocity, lookSmoothDamp);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationVelocity, lookSmoothDamp);

        character.transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
	}
}
