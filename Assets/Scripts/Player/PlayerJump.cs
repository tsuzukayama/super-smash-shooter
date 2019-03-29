using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private int jumpSpeed;
    [SerializeField] private KeyCode jumpKey;

    private bool isJumping = false;

    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    private new Collider collider;

    private float distToGround;

    // private bool isGrounded;

    private bool isLocalPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();

        distToGround = collider.bounds.extents.y;
        isLocalPlayer = transform.root.GetComponent<NetworkIdentity>().isLocalPlayer;

    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, - Vector3.up, distToGround + 0.1f);
        // return Physics.CheckCapsule(collider.bounds.center, new Vector3(collider.bounds.center.x, collider.bounds.min.y - 0.1f, collider.bounds.center.z), 0.18f);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }

        if (Input.GetKeyDown(jumpKey) && IsGrounded())
        {
            playerRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }
}
