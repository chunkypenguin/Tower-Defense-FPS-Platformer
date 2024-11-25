using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    bool canBeHit;
    [SerializeField] Player playerScript;

    private void Start()
    {
        canBeHit = true;
    }
    public void OnParticleCollision(GameObject other)
    {
        if (canBeHit)
        {
            Debug.Log("Hit");
            playerScript.TakeDamage(1);
            canBeHit = false;
            Invoke(nameof(HitTimer), 1f);
        }
    }

    public void HitTimer()
    {
        canBeHit = true;
    }
}
