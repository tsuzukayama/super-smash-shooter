using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InGameMenuController : MonoBehaviour
{
    protected static bool isOpened = false;
    public GameObject menu;

    public void ChangeMenuVisibility()
    {
        isOpened = !isOpened;
        Debug.Log("Click! " + isOpened);
        menu.SetActive(isOpened);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisconnectFromGame()
    {
        if (NetworkServer.active)
        {
            NetworkManager.singleton.StopServer();
        }
        if (NetworkClient.active)
        {
            NetworkManager.singleton.StopClient();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeMenuVisibility();
        }
    }
}
