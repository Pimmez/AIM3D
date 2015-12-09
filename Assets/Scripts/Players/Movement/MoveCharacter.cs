using UnityEngine;

public class MoveCharacter : MonoBehaviour {

    [SerializeField]
    private float speed = 0.1f;

    [SerializeField]
    private float jumpSpeed = 1.5f;

    [SerializeField]
    private float maxFallDistance = 1.5f;

    [SerializeField]
    private float shakeAmount;

    [SerializeField]
    private float shakeSpeed;

    [SerializeField]
    private float afterLandCameraShakeMultiply;

    private float hitGroundSpeedVelocity;

    [SerializeField]
    public float gravity = 0.001f;

    private Vector3 moveDirection;

    CharacterController character;

    [SerializeField]
    private Camera[] cameras;

    void Awake() {
        character = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if(!character.isGrounded) //in the air
        {
            moveDirection.y -= gravity;
            hitGroundSpeedVelocity += 0.01f;
        } else { //not in the air
            if (hitGroundSpeedVelocity > 0.05f) //just landed
            {
                foreach (Camera camera in cameras) camera.GetComponent<CameraShake>().startCamShake(shakeAmount * afterLandCameraShakeMultiply, shakeSpeed);
                if (hitGroundSpeedVelocity > maxFallDistance) Destroy(this.gameObject);
            } else { // already landed
                if (Input.GetButton("Jump")) moveDirection.y = jumpSpeed;
                else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.6f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.6f) foreach (Camera camera in cameras) camera.GetComponent<CameraShake>().startCamShake(shakeAmount, shakeSpeed);
                moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"))) * speed;
            }
            hitGroundSpeedVelocity = 0;
        }
        character.Move(moveDirection);
    }
}
