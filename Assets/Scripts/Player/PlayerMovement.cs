﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.
    public AudioClip runSound;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.

    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    private new Collider collider;

    private float distToGround;

    private float hJumping = 0;
    private float vJumping = 0;

    public PlayerAddAudios playerAudios;

    void Start()
    {
        distToGround = collider.bounds.extents.y;
        
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponentInChildren<Rigidbody>();
        collider = GetComponentInChildren<Collider>();

        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");
        playerAudios = GetComponent<PlayerAddAudios>();
    }


    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");        

        // Move the player around the scene.
        CmdMove(h, v);

        // Turn the player to face the mouse cursor.
        Vector3 playerToMouse =  CmdTurning();

        // Animate the player.
        CmdAnimating(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), playerToMouse);
    }

    void CmdMove(float h, float v)
    {
        if (!playerAudios.RunningSource.isPlaying)
            playerAudios.RunningSource.Play();

        if (h == 0 && v == 0)
            playerAudios.RunningSource.Stop();

        movement.Set(h, 0f, v);
        hJumping = h;
        vJumping = v;
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(playerRigidbody.transform.position + movement);
        // CmdMoveOnClient(playerRigidbody.transform.position + movement);        
    }

    void CmdMoveOnClient(Vector3 movement)
    {
        playerRigidbody.MovePosition(movement);
    }

    Vector3 CmdTurning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        Vector3 playerToMouse = new Vector3();
        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            playerToMouse = floorHit.point - playerRigidbody.transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);

        }

        return playerToMouse;
    }

    [Command]
    void CmdAnimating(float horizontal, float vertical, Vector3 playerToMouse)
    {
        Vector3 axisVector = new Vector3(horizontal, 0, vertical);

        float fowardMagnitude = 0, rightMagnitude = 0;

        if (axisVector.magnitude > 0)
        {
            Vector3 normalizedLookingAt = playerToMouse;
            normalizedLookingAt.Normalize();
            fowardMagnitude = Mathf.Clamp(Vector3.Dot(axisVector, normalizedLookingAt), -1, 1);


            Vector3 perpendicularLookingAt = new Vector3(normalizedLookingAt.z, 0, -normalizedLookingAt.x);            
            rightMagnitude = Mathf.Clamp(Vector3.Dot(axisVector, perpendicularLookingAt), -1, 1);
        }
        // Create a boolean that is true if either of the input axes is non-zero.

        // Tell the animator whether or not the player is walking.
        anim.SetFloat("Right", rightMagnitude);
        anim.SetFloat("Forward", fowardMagnitude);
        RpcAnimating(rightMagnitude, fowardMagnitude);
    }

    [ClientRpc]
    void RpcAnimating(float right, float forward)
    {
        anim.SetFloat("Right", right);
        anim.SetFloat("Forward", forward);
    }

    [Command]
    public void CmdPush(Vector3 direction, Vector3 point)
    {
        RpcPush(direction, point);
    }

    [ClientRpc]
    public void RpcPush(Vector3 direction, Vector3 point)
    {
        playerRigidbody.AddForceAtPosition(direction, point);
    }
}
