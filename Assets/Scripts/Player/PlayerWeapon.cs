using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Transform Arm;

    void Start()
    {
        transform.parent = Arm.transform;
    }
}
