using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathChildren : MonoBehaviour
{
    private bool isPlayerDead;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlaneKill" && !isPlayerDead)
        {
            transform.parent.GetComponent<PlayerDeath>().CollisionDetected(this);
            isPlayerDead = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        isPlayerDead = false;
    }
}
