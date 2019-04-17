using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.

    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    private new Collider collider;

    private float distToGround;

    private float hJumping = 0;
    private float vJumping = 0;

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
        CmdTurning();

        // Animate the player.
        CmdAnimating(h, v);
    }

    void CmdMove(float h, float v)
    {
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

    void CmdTurning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        float mouseXPos = Input.GetAxis("Mouse X");
        float mouseYPos = Input.GetAxis("Mouse Y");

        // Debug.Log(string.Format("mouse X: {0}, mouse Y: {1}", mouseXPos, mouseYPos));
        // Debug.Log(string.Format("mouse Position: {0}", Input.mousePosition));

        Vector3 lookPosition = new Vector3(playerRigidbody.transform.eulerAngles.x, Mathf.Atan2(mouseXPos, mouseYPos) * Mathf.Rad2Deg, playerRigidbody.transform.eulerAngles.z);

        // Debug.Log("lookPosition: " + lookPosition + ", mousePosition: " + Input.mousePosition);     
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;
        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - playerRigidbody.transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void CmdAnimating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        // anim.SetBool("IsWalking", walking);
    }

    [Command]
    public void CmdPush(Vector3 direction, Vector3 point)
    {
        playerRigidbody.AddForceAtPosition(direction, point);
        RpcPush(direction, point);
    }

    [ClientRpc]
    public void RpcPush(Vector3 direction, Vector3 point)
    {
        playerRigidbody.AddForceAtPosition(direction, point);
    }
}
