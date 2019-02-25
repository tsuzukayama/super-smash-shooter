using UnityEngine;
using Mirror;

public class NetworkHandler : NetworkBehaviour
{
    public Camera camera;

    void Start()
    {

        if (isLocalPlayer)
        {
            GameObject.Find("LobbyCamera").SetActive(false);
            return;
        }
        // DISABLE CAMERA AND CONTROLS HERE (BECAUSE THEY ARE NOT ME)
        camera.enabled = false;
        //GetComponent<PlayerControls>().enabled = false;
        //GetComponent<PlayerMovement>().enabled = false;
    }
}