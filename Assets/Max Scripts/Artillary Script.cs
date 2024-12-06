using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtillaryScript : MonoBehaviour
{
    public GameObject projectilePrefab;  // The projectile (dart) the tower will shoot
    public float fireRate = 1f;          // Time in seconds between each shot
    public float shootingRadius = 5f;    // The radius in which the tower can target enemies
    public float projectileSpeed = 10f;  // Speed at which the projectile moves
    public float projectileLifetime = 5f; // Time in seconds before the projectile despawns
    private float fireCooldown;          // Timer to control fire rate

    private Transform target;            // Target enemy
    private float angleToTarget;         // Angle to target enemy

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        // Detect enemies within the shooting radius (spherical range)
        DetectTarget();

        // If fire cooldown is over and there is a target, shoot at the target
        if (fireCooldown <= 0 && target != null)
        {
            // Rotate to face the target
            RotateToTarget();

            // Shoot at the target
            ShootAtTarget();
            
            fireCooldown = 1f / fireRate; // Reset cooldown based on fire rate
        }
    }

    // Detect the closest enemy within the shooting radius (spherical range)
    void DetectTarget()
    {
        // Find all colliders in the shooting radius
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, shootingRadius);

        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        // Loop through all enemies within the radius and find the nearest one
        foreach (Collider collider in enemiesInRange)
        {
            if (collider.CompareTag("Enemy") || collider.CompareTag("Player"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = collider.transform;
                }
            }
        }

        // Set the target to the nearest enemy
        target = nearestEnemy;
    }

    // Rotate the tower to face the target
    void RotateToTarget()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;

            // Create a rotation that looks in the direction of the target
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Apply the rotation to the tower (smooth rotation can be added if needed)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
    }

    // Shoot a projectile towards the target enemy
    void ShootAtTarget()
    {
        if (target != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Instantiate the projectile at the tower's position
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Set the projectile's velocity in the direction of the target
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }

            // Destroy the projectile after 'projectileLifetime' seconds
            Destroy(projectile, projectileLifetime);

            // Add the OnCollisionEnter behavior to handle destruction of enemies and projectiles
            Projectile projectileScript = projectile.AddComponent<Projectile>();
            projectileScript.projectileLifetime = projectileLifetime;
        }
    }

    // Visualization of the shooting range for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRadius);
    }
}



