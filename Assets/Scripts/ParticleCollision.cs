using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hi");
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
    }
}
