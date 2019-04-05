using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerDeath : NetworkBehaviour
{    
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    Collider collider;

    public bool isPlayerDead = false;

    private Vector3 initialPosition;

    void Start()
    {
        playerRigidbody = GetComponentInChildren<Rigidbody>();
        initialPosition = playerRigidbody.transform.position;
        collider = GetComponentInChildren<Collider>();        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "PlaneKill")
        {
            isPlayerDead = true;
            Debug.Log("is dead");
            GameManager.singleton.SetEndText("You are dead");
        }
    }
}
