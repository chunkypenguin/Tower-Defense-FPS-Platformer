using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb; // Use Rigidbody for 3D

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        // Ensure path exists and is not empty
        if (LevelManager.main.path.Length > 0)
        {
            target = LevelManager.main.path[pathIndex];
        }
        else
        {
            Debug.LogError("Path is empty in LevelManager");
        }
    }

    private void Update()
    {
        // If the enemy reaches the target waypoint
        if (Vector3.Distance(target.position, transform.position) < 0.1f)
        {
            pathIndex++;

            // If the enemy has reached the end of the path
            if (pathIndex >= LevelManager.main.path.Length)
            {
                Destroy(gameObject); // Destroy enemy when it reaches the end of the path
                return;
            }

            target = LevelManager.main.path[pathIndex];
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Get direction towards the target (in 3D space)
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed; // Set Rigidbody velocity based on direction and speed
        }
    }
}
