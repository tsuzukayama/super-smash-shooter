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

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        distToGround = collider.bounds.extents.y;
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

// Update is called once per frame
    void FixedUpdate()
    {
        if (IsGrounded())
        {
            isJumping = false;
        }
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            Debug.Log("jumpando");
            isJumping = true;
            playerRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }

    }
}
