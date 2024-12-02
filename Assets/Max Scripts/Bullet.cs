using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float projectileLifetime; // Lifetime of the projectile

    // Make sure the collision is detected when using physics-based interactions
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
        }
    }
}
