using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerDeath : NetworkBehaviour
{    
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    Collider collider;
    
    public bool isPlayerDead;
    // private GameManagement gameManagement;

    private Vector3 initialPosition;

    private uint _netId;
    private bool hasPlayerBeenAdded;


    void Awake()
    {
        playerRigidbody = GetComponentInChildren<Rigidbody>();
        initialPosition = playerRigidbody.transform.position;
        collider = GetComponentInChildren<Collider>();
        // gameManagement = GameObject.Find("/GameManager").GetComponent<GameManagement>();
        // isPlayerDead = false;           
    }    

    void FixedUpdate()
    {        
        if (!isLocalPlayer)
        {
            return;
        }

        CmdAddPlayer(netId.Value);

        if (isPlayerDead)
        {
            ShowDeadText();
            CmdRemovePlayer(netId.Value);
        }
        if (GameManagement.Instance.HasPlayerWon(netId.Value))
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
        if(!GameManagement.Instance.Players.Contains(_netId))
            GameManagement.Instance.Players.Add(_netId);
    }

    [Command]
    void CmdRemovePlayer(uint _netId)
    {
        RpcRemovePlayer(_netId);
    }

    [ClientRpc]
    void RpcRemovePlayer(uint _netId)
    {
        GameManagement.Instance.Players.Remove(_netId);
    }

    public void CollisionDetected(PlayerDeathChildren childScript)
    {
        isPlayerDead = true;
    }

    public void ShowDeadText()
    {
        GameManagement.Instance.endText.text = "You are dead";
    }

    public void ShowWinText()
    {
        GameManagement.Instance.endText.text = "You Win";
    }
}
