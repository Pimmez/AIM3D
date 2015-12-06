using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCharacter : MonoBehaviour {

    [SerializeField]
    private float speed = 0.1f;

    [SerializeField]
    private float jumpSpeed = 1.5f;

    [SerializeField]
    private float maxFallDistance = 1.5f;

    [SerializeField]
    private float shakeStrenght;

    private float hitGround;

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
            hitGround += 0.01f;
        } else { //not in the air
            if (hitGround > maxFallDistance) Destroy(this.gameObject);
            else if (hitGround > maxFallDistance / 4) foreach (Camera camera in cameras) camera.GetComponent<CameraShake>().Shake(shakeStrenght);
            else if (Input.GetButton("Jump")) moveDirection.y = jumpSpeed;
            //set the input directions to world space, multiply by speed + hitGround value)
            moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") + hitGround, moveDirection.y, Input.GetAxis("Vertical") + hitGround)) * speed;
            hitGround = 0;
        }
        character.Move(moveDirection);
    }
}
