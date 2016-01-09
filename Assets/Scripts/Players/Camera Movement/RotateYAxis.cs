using UnityEngine;

public class RotateYAxis : MonoBehaviour {

    [SerializeField]
    protected float lookSensitivity = 5;

    [SerializeField]
    protected float lookSmoothDamp = 0.1f;

    [SerializeField]
    protected string inputHorizontal = "Look X";

    protected float yRotation;

    protected float currentYRotation;

    private float yRotationVelocity;

    // Update is called once per frame
    protected virtual void FixedUpdate() {
        RotateY();

        //update the camera rotation
        UpdateRotation();
    }

    protected virtual void RotateY() {
        yRotation += Input.GetAxis(inputHorizontal) * lookSensitivity;

        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationVelocity, lookSmoothDamp);
    }

    protected virtual void UpdateRotation() {
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }
}
