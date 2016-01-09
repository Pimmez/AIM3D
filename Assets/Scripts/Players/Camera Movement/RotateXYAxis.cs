using UnityEngine;

public class RotateXYAxis : RotateYAxis
{
    [SerializeField]
    protected string inputVertical = "Mouse Y";

    private float xRotation;

    protected float currentXRotation;

    private float xRotationVelocity;

    private Transform character;

    // Use this for initialization
    void Awake()
    {
        //if the movement is horizontal, get the parent transform so we can rotate it.
        character = transform.parent;
    }

    protected override void FixedUpdate()
    {
        //rotate over both x & y
        RotateX();

        //update the rotation
        base.FixedUpdate();
    }

    protected override void RotateY()
    {
        base.RotateY();

        //rotates the whole player in Y direction
        character.transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }

    protected void RotateX()
    {
        xRotation -= Input.GetAxis(inputVertical) * lookSensitivity;

        //clamp the X , so the camera cant go around backwards
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVelocity, lookSmoothDamp);
    }

    protected override void UpdateRotation()
    {
        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
    }
}
