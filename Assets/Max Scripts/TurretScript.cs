using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerScript : MonoBehaviour
{
    public GameObject projectilePrefab;  // The projectile the tower will shoot
    public float fireRate = 1f;          // Time in seconds between each shot
    public float shootingRadius = 5f;    // The radius in which the tower shoots
    public int numberOfShots = 8;        // The number of shots fired in the circle
    public float projectileSpeed = 10f;  // Speed at which the projectile moves
    public float projectileLifetime = 5f; // Time in seconds before the projectile despawns
    private float fireCooldown;          // Timer to control fire rate

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        // If fire cooldown is over, shoot in a circular pattern
        if (fireCooldown <= 0)
        {
            ShootInCircle();
            fireCooldown = 1f / fireRate; // Reset cooldown based on fire rate
        }
    }

    void ShootInCircle()
    {
        float angleStep = 360f / numberOfShots;  // Angle between each shot

        for (int i = 0; i < numberOfShots; i++)
        {
            float angle = i * angleStep; // Calculate angle for each shot

            // Convert the angle to a direction vector in world space
            Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle));
            Vector3 shootPosition = transform.position + direction * shootingRadius;

            // Create and fire the projectile
            GameObject projectile = Instantiate(projectilePrefab, shootPosition, Quaternion.identity);

            // Set the projectile's velocity in the calculated direction
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
}

public class Projectile : MonoBehaviour
{
    public float projectileLifetime; // Lifetime of the projectile
    private EnemySpawner enemySpawner; // Reference to the EnemySpawner

    void Start()
    {
        // Find the EnemySpawner in the scene
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Log the object the projectile collided with
        Debug.Log("Projectile collided with: " + collision.gameObject.name);

        // Check if the projectile collides with an enemy (with the "Enemy" tag)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the projectile
            Destroy(gameObject);

            // Destroy the enemy (target) that was hit
            Destroy(collision.gameObject);

            // Notify the EnemySpawner that an enemy has been destroyed
            if (enemySpawner != null)
            {
                enemySpawner.EnemyDestroyed(); // Decrease enemiesAlive
            }
        }
    }
}
