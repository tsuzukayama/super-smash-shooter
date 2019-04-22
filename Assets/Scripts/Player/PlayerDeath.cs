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

    private NetworkInstanceId _netId;


    void Awake()
    {
        playerRigidbody = GetComponentInChildren<Rigidbody>();
        initialPosition = playerRigidbody.transform.position;
        collider = GetComponentInChildren<Collider>();
        gameManagement = GameObject.Find("/GameManager").GetComponent<GameManagement>();
        // isPlayerDead = false;        
        
    }

    void Start()
    {
        _netId = netId;
        gameManagement.Players.Add(netId);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (isPlayerDead)
        {
            ShowDeadText();
        }
        Debug.Log(gameManagement.HasPlayerWon(netId));
    }


    public void CollisionDetected(PlayerDeathChildren childScript)
    {
        isPlayerDead = true;
    }

    public void ShowDeadText()
    {
        gameManagement.endText.text = "You are dead";
    }
}
