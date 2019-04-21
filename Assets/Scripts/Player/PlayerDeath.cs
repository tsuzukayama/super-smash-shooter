using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerDeath : NetworkBehaviour
{    
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    Collider collider;
    
    public bool isPlayerDead;
    private GameManagement gameManagement;

    private Vector3 initialPosition;


    void Awake()
    {
        playerRigidbody = GetComponentInChildren<Rigidbody>();
        initialPosition = playerRigidbody.transform.position;
        collider = GetComponentInChildren<Collider>();
        gameManagement = GameObject.Find("/GameManager").GetComponent<GameManagement>();
        // isPlayerDead = false;
    }

    void FixedUpdate()
    {
        // Debug.Log("IsPlayerDead: " + isPlayerDead);
        if (!isLocalPlayer)
        {
            return;
        }
        if (isPlayerDead)
        {
            ShowDeadText();
        }
    }


    public void CollisionDetected(PlayerDeathChildren childScript)
    {
        isPlayerDead = true;
        Debug.Log("child collided");
    }

    public void ShowDeadText()
    {
        //isPlayerDead = true;
        gameManagement.endText.text = "You are dead";
    }
}
