using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class CameraFollow : MonoBehaviour
{
    // public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;           // The speed with which the camera will be following.
    Vector3 offset;                        // The initial offset from the target.

    private Transform target;
    private bool isLocalPlayer;
    private float yAxis = 0.0f;

    void Start()
    {
        target = gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.transform;
        // Calculate the initial offset.
        offset = transform.position - target.position;
        isLocalPlayer = gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            var newAxisY = yAxis + 2.0f * Input.GetAxis("Mouse X");

            if (Math.Abs(newAxisY) <= 50f)
                yAxis = newAxisY;

            var x = transform.eulerAngles.x;
            var z = transform.eulerAngles.z;
            transform.eulerAngles = new Vector3(x, yAxis, z);
        }
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            gameObject.transform.parent.gameObject.transform.Find("Camera").gameObject.SetActive(false);
            return;
        }

        var changeFactor = yAxis / 60;

        var position = new Vector3(target.position.x + changeFactor * 4f, target.position.y + changeFactor * 4f, target.position.z - Math.Abs(changeFactor) * 8f);

        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
