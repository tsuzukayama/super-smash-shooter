using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private int jumpSpeed;
    [SerializeField] private KeyCode jumpKey;

    private bool isJumping = false;

    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    private new Collider collider;

    private float distToGround;

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        distToGround = collider.bounds.extents.y;
    }

    bool IsGrounded() {
        return Physics.CheckCapsule(collider.bounds.center, new Vector3(collider.bounds.center.x, collider.bounds.min.y - 0.5f, collider.bounds.center.z), 0.5f);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            isGrounded = false;
        }
    }
}
