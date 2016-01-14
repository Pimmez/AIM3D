using UnityEngine;
using System.Collections.Generic;

public class MoveCharacter : MonoBehaviour {

    [SerializeField]
    private float baseSpeed = 2.7f;

    [SerializeField]
    private float runSpeedMultiply = 1.5f;

    [SerializeField]
    private float crouchSpeedMultiply = 0.5f;

    private bool crouching;

    private float standingSizeY = 1;

    private Crouch crouch;

    [SerializeField]
    private float jumpSpeed = 6;

    [SerializeField]
    private float maxFallTime = 1500f;

    [SerializeField]
    private float fallingHitGroundTreshold = 0.6f;

    [SerializeField]
    private float shakeTreshold = 0.15f;

    [SerializeField]
    private float shakeAmount = 0.05f;

    [SerializeField]
    private float shakeTime = 0.2f;

    [SerializeField]
    private float handShakeDivide = 3;

    [SerializeField]
    private float afterLandCameraShakeMultiply = 2;

    private float hitGroundSpeedVelocity;

    [SerializeField]
    private float gravity = 0.18f;

    private Vector3 moveDirection;

    [SerializeField]
    private GameObject camera;

    private Shake cameraShake;

    [SerializeField]
    private GameObject[] hands;

    private List<Shake> handsShake = new List<Shake>();

    CharacterController character;

    [SerializeField]
    private int footstepSoundTimeMultiplier = 35;

    [SerializeField]
    private float maxMoveNoiseTime = 30;

    private float moveNoiseTimer;

    [SerializeField]
    private int baseMoveNoiseStrength = 7;

    [SerializeField]
    private int jumpSoundStrength = 15;

    Noise noise;

    DetectVR detectVR;

    SoundTimer soundTimer;

    void Awake() {
        standingSizeY = transform.localScale.y;

        detectVR = GetComponent<DetectVR>();

        soundTimer = GetComponent<SoundTimer>();

        character = GetComponent<CharacterController>();
        noise = GetComponent<Noise>();
        crouch = GetComponent<Crouch>();

        cameraShake = camera.GetComponent<Shake>();

        //put each shake script of hands in handsShake
        foreach (GameObject hand in hands) {
            handsShake.Add(hand.GetComponent<Shake>());
        }

        crouchSpeedMultiply = standingSizeY * crouchSpeedMultiply;
    }

    void Update()
    {
        if (!character.isGrounded) //in the air
        {
            //apply gravity
            moveDirection.y -= gravity * Time.deltaTime;

            //count the time in the air
            hitGroundSpeedVelocity += 1 * Time.deltaTime;
        }
        else
        { //not in the air
            if (hitGroundSpeedVelocity > fallingHitGroundTreshold) //just landed
            {
                ShakeObjs(afterLandCameraShakeMultiply, 1);

                noise.NoiseArea(jumpSoundStrength);

                // the higher hitGroundSpeedVelocity is, the longer high that means 
                if (hitGroundSpeedVelocity > maxFallTime) Destroy(this.gameObject);

            }
            else
            { // already landed
                //speed value is used to store the current speed
                float speedValue = baseSpeed;

                //speed multiplier is used to change the speedValue when you sprint or you crouch, or when you walk normal it is 1
                float speedMultiplier = 1;

                if (Input.GetButtonDown("Jump")) moveDirection.y = jumpSpeed;

                //set the speedMultiplier to either run speed or crouch speed
                else if (Input.GetButton("Run"))
                {
                    //set crouching on false when you start sprinting
                    if(crouching) switchCrouch(false);

                    //set speedmultiplier to runningspeed (higher than 1)
                    speedMultiplier = runSpeedMultiply;
                }
                else if (Input.GetButtonDown("Crouch"))
                {
                    //give the opposite of the current crouching state, so that it can switch the other state
                    switchCrouch(!crouching);
                }

                //if crouching, set the speed multiplier to crouching speed (something below 1)
                else if (crouching) speedMultiplier = crouchSpeedMultiply;

                //if the positive value of horizontal or vertical input is higher than the shakeTreshold, shake each camera  this object.
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > shakeTreshold || Mathf.Abs(Input.GetAxis("Vertical")) > shakeTreshold)
                {
                    ShakeObjs(speedMultiplier, speedMultiplier);

                    MakeNoise(speedMultiplier);
                } 

                //move using the Horizontal or Vertical values of input and multiplying them by speed
                moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") * (speedValue * speedMultiplier), moveDirection.y, Input.GetAxis("Vertical") * (speedValue * speedMultiplier)));
                //print(Input.GetAxis("Horizontal"));
            }
            hitGroundSpeedVelocity = 0;
        }
        character.Move(moveDirection * Time.deltaTime);
    }

    private void ShakeObjs(float _multiplyAmount, float _divideTime) {

        //if we have a VR connected, we need to play the footsteps manually
        if (detectVR.OculusPresent)
        {
            int footstepTime = Mathf.RoundToInt((1 / _multiplyAmount) * footstepSoundTimeMultiplier);
            soundTimer.StartWaitForTime(footstepTime);
        }
        //else we add a shake to the camera, and the footstep sounds is handled by the shakescript
        else
        {
            cameraShake.StartShake(shakeAmount * _multiplyAmount, shakeTime / _divideTime);

            foreach (Shake handShake in handsShake)
            {
                handShake.StartShake(shakeAmount * (_multiplyAmount / handShakeDivide), shakeTime / _divideTime);
            }
        }
    }

    //switches the crouch state to the value that it is given in the parameters
    private void switchCrouch(bool _currentCrouchingState) {
        if (!crouch.GetChangingCrouch)
        {
            //make variable in which we store the new Size the player is going to be resized to.
            float newSizeY;
            //crouch speed multiply is also used for the new player size
            if (!crouching) newSizeY = crouchSpeedMultiply;
            else newSizeY = standingSizeY;

            if (crouch.CheckIfCanCrouch(newSizeY))
            {
                //switch crouching boolean
                crouching = _currentCrouchingState;

                //activate keepYScale coroutine in each hand. which keeps the scale as long as the player isnt as big as newSizeY
                foreach (GameObject hand in hands)
                {
                    hand.GetComponent<KeepYScale>().StartKeepYScale(newSizeY);
                }

                //resizes the player to the new size
                crouch.ChangeCrouchState(newSizeY);
            }
        }
    }

    private void MakeNoise(float _strength) {
        //increase the timer when we are moving
        moveNoiseTimer += 1 * Time.deltaTime;

        //if the timer is higher then then maxMoveNoise
        if (moveNoiseTimer > maxMoveNoiseTime)
        {
            noise.NoiseArea(baseMoveNoiseStrength * _strength);
            moveNoiseTimer = 0;
        }
    }
}
