using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{    
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    Collider collider;
    
    private bool isLocal;
    private bool isPlayerDead;
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
        if (isPlayerDead)
        {
            ShowDeadText();
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);        
        if (collision.gameObject.name == "PlaneKill")
        {
            // isPlayerDead = true;
            ShowDeadText();
        }
    }
    
    public void ShowDeadText()
    {
        //isPlayerDead = true;
        Debug.Log("is dead");
        gameManagement.endText.text = "You are dead";
    }
}
