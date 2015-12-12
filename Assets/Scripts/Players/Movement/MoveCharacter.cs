using UnityEngine;

public class MoveCharacter : MonoBehaviour {

    [SerializeField]
    private float speed = 0.1f;

    [SerializeField]
    private float jumpSpeed = 1.5f;

    [SerializeField]
    private float maxFallTime = 1500f;

    [SerializeField]
    private float fallingHitGroundSpeedVelocity = 15f;

    [SerializeField]
    private float shakeTreshold = 0.4f;

    [SerializeField]
    private float shakeAmount;

    [SerializeField]
    private float shakeSpeed;

    [SerializeField]
    private float afterLandCameraShakeMultiply;

    private float hitGroundSpeedVelocity;

    [SerializeField]
    private float gravity = 0.001f;

    private Vector3 moveDirection;

    [SerializeField]
    private Camera[] cameras;

    CharacterController character;

    [SerializeField]
    private int jumpSoundStrength = 15;

    Noise noise;

    void Awake() {
        character = GetComponent<CharacterController>();
        noise = GetComponent<Noise>();
    }

    void FixedUpdate()
    {
        if(!character.isGrounded) //in the air
        {
            moveDirection.y -= gravity;
            hitGroundSpeedVelocity += 1f;
        } else { //not in the air
            if (hitGroundSpeedVelocity > fallingHitGroundSpeedVelocity) //just landed
            {
                foreach (Camera camera in cameras) camera.GetComponent<CameraShake>().startCamShake(shakeAmount * afterLandCameraShakeMultiply, shakeSpeed);
                noise.NoiseArea(jumpSoundStrength);
                // the higher hitGroundSpeedVelocity is, the longer high that means 
                if (hitGroundSpeedVelocity > maxFallTime) Destroy(this.gameObject);
            } else { // already landed
                if (Input.GetButton("Jump")) moveDirection.y = jumpSpeed;

                //if you didnt jump and the positive value of horizontal or vertical input is higher than the shakeTreshold, shake each camera  this object.
                else if (Mathf.Abs(Input.GetAxis("Horizontal")) > shakeTreshold || Mathf.Abs(Input.GetAxis("Vertical")) > shakeTreshold)
                {
                    foreach (Camera camera in cameras)
                    {
                        camera.GetComponent<CameraShake>().startCamShake(shakeAmount, shakeSpeed);
                    }
                }
                //move using the Horizontal or Vertical values of input and multiplying them by speed
                moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"))) * speed;
            }
            hitGroundSpeedVelocity = 0;
        }
        character.Move(moveDirection);
    }
}
