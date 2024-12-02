using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerScript : MonoBehaviour
{
    public Transform gun;  // Reference to the gun object (if it's separate from the turret)
    public float rotationSpeed = 5f; // Speed at which the gun rotates
    public float detectionRange = 10f; // Detection range for finding enemies
    public LayerMask enemyLayer;  // Layer mask for enemies

    private Transform targetEnemy;  // The current target enemy

    void Update()
    {
        // Find the nearest enemy within detection range
        FindTargetEnemy();

        // If there is a target enemy, rotate the gun towards it
        if (targetEnemy != null)
        {
            RotateGun();
        }
    }

    // Method to find the nearest enemy in range
    void FindTargetEnemy()
    {
        // Check for enemies in range from the turret's position
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);

        if (enemiesInRange.Length == 0)
        {
            targetEnemy = null; // No enemies found
            return;
        }

        // Find the nearest enemy
        float closestDistance = Mathf.Infinity;
        foreach (var enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                targetEnemy = enemy.transform;
            }
        }
    }

    // Method to rotate the gun towards the target enemy
    void RotateGun()
    {
        // Get the direction from the turret to the enemy
        Vector2 directionToEnemy = targetEnemy.position - transform.position;  // Turret's position, not gun's

        // Calculate the rotation angle in radians
        float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;

        // Smoothly rotate the gun towards the target
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        gun.rotation = Quaternion.Slerp(gun.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Debug method to visualize the detection range
    void OnDrawGizmosSelected()
    {
        // Draw detection range from the turret's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);  // Range drawn from turret center
    }
}
