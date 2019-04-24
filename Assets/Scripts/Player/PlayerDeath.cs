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

    private uint _netId;
    private bool hasPlayerBeenAdded;


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
        if (!isLocalPlayer)
        {
            return;
        }
        if (!hasPlayerBeenAdded)
        {
            gameManagement.Players.Add(netId.Value);
            hasPlayerBeenAdded = true;
        }
        if (isPlayerDead)
        {
            ShowDeadText();
            gameManagement.Players.Remove(netId.Value);
        }
        if (gameManagement.HasPlayerWon(netId.Value))
        {
            ShowWinText();
        }        
    }


    [Command]
    void CmdAddPlayer(uint _netId)
    {
        RpcAddPlayer(_netId);
    }

    [ClientRpc]
    void RpcAddPlayer(uint _netId)
    {        
        gameManagement.Players.Add(_netId);
    }

    [Command]
    void CmdRemovePlayer(uint _netId)
    {
        RpcRemovePlayer(_netId);
    }

    [ClientRpc]
    void RpcRemovePlayer(uint _netId)
    {
        gameManagement.Players.Remove(_netId);
    }

    public void CollisionDetected(PlayerDeathChildren childScript)
    {
        isPlayerDead = true;
    }

    public void ShowDeadText()
    {
        gameManagement.endText.text = "You are dead";
    }

    public void ShowWinText()
    {
        gameManagement.endText.text = "You Win";
    }
}
