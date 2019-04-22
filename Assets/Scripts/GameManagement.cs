using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public Text endText;

    private static GameManagement _instance;
    public static GameManagement Instance { get { return _instance; } }

    public List<NetworkInstanceId> Players = new List<NetworkInstanceId>();

    public bool hasGameStarted;

    private void Awake()
    {               
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void FixedUpdate()
    {
        // Players = NetworkManager.singleton.client.connection.playerControllers.Select(p => p.playerControllerId);
        // Players.ForEach(p => Debug.Log(p));

        if (Players.Count > 2) hasGameStarted = true;
    }

    public bool HasPlayerWon(NetworkInstanceId playerControllerId)
    {
        bool isPlayerAlive = false;
        Players.ForEach(p =>
        {
            if (p == playerControllerId) isPlayerAlive = true;
        });
        return hasGameStarted && isPlayerAlive && Players.Count == 1;
    }

    public void SetEndText(string text)
    {
        endText.text = text;
    }
}
