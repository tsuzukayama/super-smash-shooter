using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerDeath : MonoBehaviour
{    
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    Collider collider;

    private bool isPlayerDead = false;

    private Vector3 initialPosition;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        initialPosition = playerRigidbody.transform.position;
        collider = GetComponent<Collider>();

    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "PlaneKill")
        {
            isPlayerDead = true;
            Debug.Log("is dead");
            playerRigidbody.transform.position = initialPosition;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.tag);
        if (collider.gameObject.tag == "Kill")
        {
            isPlayerDead = true;
            Debug.Log("is dead");
            playerRigidbody.transform.position = initialPosition;
        }
    }
}
