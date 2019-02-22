using UnityEngine.Networking;

public class NetworkHandler : NetworkBehaviour
{
    public Camera camera;

    void Awake()
    {
        camera.enabled = false;
    }

    public override void OnStartLocalPlayer()
    {
        camera.enabled = true;
    }
}
