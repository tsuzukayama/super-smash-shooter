using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour
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

    public Transform gunTransform;

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        gunLine = GetComponentInChildren<LineRenderer>();
        gunLight = GetComponentInChildren<Light>();
    }

    private void Start()
    {
        Debug.Log(transform.root.name);
    }

    void FixedUpdate()
    {
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
            CmdShoot();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            CmdDisableEffects();
        }
    }

    [Command]
    public void CmdDisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
       // RpcDisableEffects(false, false);
    }

    [ClientRpc]
    public void RpcDisableEffects(bool gunLineEnabled, bool gunLightEnabled)
    {
        gunLine.enabled = gunLineEnabled;
        gunLight.enabled = gunLightEnabled;
    }

    [ClientRpc]
    void RpcStartShooting(Vector3 position, Vector3 forward)
    {
        // Play the gun shot audioclip.
        // gunAudio.Play();

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        // gunParticles.Stop();
        // gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = position;
        shootRay.direction = forward;
    }

    [Command]
    void CmdShoot()
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
        gunLine.SetPosition(0, gunTransform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = gunTransform.position;
        shootRay.direction = gunTransform.forward;

        RpcStartShooting(gunTransform.position, gunTransform.forward);
               
        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.SphereCast(shootRay.origin, 3f, shootRay.direction, out shootHit, range, shootableMask))
        {

            // Try and find an EnemyHealth script on the gameobject hit.
            PlayerMovement playerDamage = shootHit.collider.transform.root.GetComponent<PlayerMovement>();
            NetworkIdentity networkIdentity = shootHit.collider.transform.root.GetComponent<NetworkIdentity>();

            NetworkConnection networkConnection = shootHit.collider.transform.root.GetComponent<NetworkIdentity>().connectionToClient;
            // If the EnemyHealth component exist...
            if (playerDamage != null)
            {
                Debug.Log("hit!!!!");

                playerDamage.CmdPush(300 * shootRay.direction, shootHit.point);
                // pushEnemy(playerDamage.transform.parent.GetComponentInChildren<Rigidbody>());
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
    
    void CmdPush()
    {

    }
}