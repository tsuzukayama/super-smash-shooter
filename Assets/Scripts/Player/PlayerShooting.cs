﻿using Mirror;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float range = 100f;                      // The distance the gun can fire.

    float timer;                                    // A timer to determine when to fire.
    Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    LineRenderer gunLine;                           // Reference to the line renderer.
    Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

    bool isLocalPlayer;
    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();

        
    }

    private void Start()
    {
        isLocalPlayer = transform.root.GetComponent<NetworkIdentity>().isLocalPlayer;
        Debug.Log(transform.root.name);
    }

    void Update()
    {
        // Debug.Log("Is local player: " + isLocalPlayer);
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            // ... shoot the gun.
            Shoot();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        // gunAudio.Play();

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        // gunParticles.Stop();
        // gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {

            // Try and find an EnemyHealth script on the gameobject hit.
            GameObject enemy = shootHit.collider.gameObject;

            // If the EnemyHealth component exist...
            if (enemy != null)
            {
                Debug.Log("hit!!!!");
                pushEnemy(enemy.transform.parent.GetComponent<Rigidbody>());
                // ... the enemy should take damage.
                // enemy.TakeDamage(damagePerShot, shootHit.point);
            }

            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    [ClientRpc]
    void pushEnemy(Rigidbody enemy)
    {
        enemy.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
}