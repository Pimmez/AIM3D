using UnityEngine;
using System.Collections;

public class MoveCharacter : MonoBehaviour {

    [SerializeField]
    private float baseSpeed = 2.7f;

    [SerializeField]
    private float runSpeedMultiply = 1.5f;

    [SerializeField]
    private float crouchSpeedMultiply = 0.5f;

    private bool crouching;

    private float standingSizeY = 1;

    [SerializeField]
    private float crouchingSpeedMultiplier = 0.66f;

    private Crouch crouch;

    [SerializeField]
    private float jumpSpeed = 6;

    [SerializeField]
    private float maxFallTime = 1500f;

    [SerializeField]
    private float fallingHitGroundTreshold = 0.6f;

    [SerializeField]
    private float shakeTreshold = 0.3f;

    [SerializeField]
    private float shakeAmount = 0.1f;

    [SerializeField]
    private float shakeSpeed = 0.2f;

    [SerializeField]
    private float afterLandCameraShakeMultiply = 2;

    private float hitGroundSpeedVelocity;

    [SerializeField]
    private float gravity = 0.18f;

    private Vector3 moveDirection;

    [SerializeField]
    private Camera[] cameras;

    CharacterController character;

    [SerializeField]
    private float maxMoveNoiseTime = 30;

    private float moveNoiseTimer;

    [SerializeField]
    private int baseMoveNoiseStrength = 7;

    [SerializeField]
    private int jumpSoundStrength = 15;

    Noise noise;

    void Awake() {
        standingSizeY = transform.localScale.y;

        character = GetComponent<CharacterController>();
        noise = GetComponent<Noise>();
        crouch = GetComponent<Crouch>();

        crouchingSpeedMultiplier = standingSizeY * crouchingSpeedMultiplier;
    }

    void Update()
    {
        if(!character.isGrounded) //in the air
        {
            //aply gravity
            moveDirection.y -= gravity;

            //count the time in the air
            hitGroundSpeedVelocity += 1 * Time.deltaTime;
        } else { //not in the air
            if (hitGroundSpeedVelocity > fallingHitGroundTreshold) //just landed
            {
                foreach (Camera camera in cameras) camera.GetComponent<CameraShake>().startCamShake(shakeAmount * afterLandCameraShakeMultiply, shakeSpeed);

                noise.NoiseArea(jumpSoundStrength);

                // the higher hitGroundSpeedVelocity is, the longer high that means 
                if (hitGroundSpeedVelocity > maxFallTime) Destroy(this.gameObject);

            } else { // already landed
                //speed value is used to store the current speed
                float speedValue = baseSpeed;

                //speed multiplier is used to change the speedValue when you sprint or you crouch, or when you walk normal it is 1
                float speedMultiplier = 1;

                //set the speedMultiplier to either run speed or crouch speed
                if (Input.GetButton("Run")) speedMultiplier = runSpeedMultiply;
                else if (Input.GetButtonDown("Crouch"))
                {
                    if (!crouch.GetChangingCrouch)
                    {
                        if (crouching) crouch.ChangeCrouchState(standingSizeY);
                        else crouch.ChangeCrouchState(crouchingSpeedMultiplier);

                        //make crouching the opposite of what it is now.
                        crouching = !crouching;
                    }
                }

                else if(crouching) speedMultiplier = crouchSpeedMultiply;

                else if (Input.GetButtonDown("Jump")) moveDirection.y = jumpSpeed;

                //if the positive value of horizontal or vertical input is higher than the shakeTreshold, shake each camera  this object.
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > shakeTreshold || Mathf.Abs(Input.GetAxis("Vertical")) > shakeTreshold)
                {
                    foreach (Camera camera in cameras)
                    {
                        camera.GetComponent<CameraShake>().startCamShake(shakeAmount * speedMultiplier, shakeSpeed / speedMultiplier);
                    }

                    //increase the timer when we are moving
                    moveNoiseTimer += 1 * Time.deltaTime;

                    //if the timer is higher then then maxMoveNoise
                    if (moveNoiseTimer > maxMoveNoiseTime)
                    {
                        noise.NoiseArea(baseMoveNoiseStrength * speedMultiplier);
                        moveNoiseTimer = 0;
                    }
                }

                //move using the Horizontal or Vertical values of input and multiplying them by speed
                moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") * (speedValue * speedMultiplier), moveDirection.y, Input.GetAxis("Vertical") * (speedValue * speedMultiplier)));
            }
            hitGroundSpeedVelocity = 0;
        }
        character.Move(moveDirection * Time.deltaTime);
    }
}
