using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField]
    private string inputHorizontal = "Mouse X";

    [SerializeField]
    private string inputVertical = "Mouse Y";

    private bool rotateVertical = true;

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

        //disable vertical camera movement if there is VR detected
        //because if VR is present, the vertical camera movement is handled by the VR
        if(transform.parent.GetComponent<DetectVR>().VRIsPresent)
        {
            rotateVertical = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotateVertical) {
            //vertical camera rotation
            xRotation += Input.GetAxis(inputVertical) * lookSensitivity;

            xRotation = Mathf.Clamp(xRotation, -90, 90);

            currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVelocity, lookSmoothDamp);
        }

        //horizontal camera rotation
        yRotation += Input.GetAxis(inputHorizontal) * lookSensitivity;

        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationVelocity, lookSmoothDamp);

        character.transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        transform.rotation = Quaternion.Euler(-currentXRotation, currentYRotation, 0);
    }
}