using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    // public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.

    Vector3 offset;                     // The initial offset from the target.

    private bool isLocalPlayer;

    private Transform target;

    void Start()
    {
        target = gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.transform;
        // Calculate the initial offset.
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            gameObject.transform.parent.gameObject.transform.Find("Camera").gameObject.SetActive(false);
            return;
        }

        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
