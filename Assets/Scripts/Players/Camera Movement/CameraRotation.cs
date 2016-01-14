using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField]
    private string inputHorizontal = "Look X";

    [SerializeField]
    private string inputVertical = "Look Y";

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
    void Awake()
    {
        character = transform.parent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yRotation += Input.GetAxis(inputHorizontal) * lookSensitivity;
        xRotation += Input.GetAxis(inputVertical) * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVelocity, lookSmoothDamp);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationVelocity, lookSmoothDamp);

        character.transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        transform.rotation = Quaternion.Euler(-currentXRotation, currentYRotation, 0);
    }
}