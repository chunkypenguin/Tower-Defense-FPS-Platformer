using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public AudioClip destroySound; // The sound to play when destroyed
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Add an AudioSource component if not already attached
        audioSource = GetComponent<AudioSource>();
        
        // If there is no AudioSource, create one dynamically
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the clip for the AudioSource if it is not set already
        if (destroySound != null && audioSource != null)
        {
            audioSource.clip = destroySound;
        }
    }

    public void DestroyObject()
    {
        // Play the sound before destroying the object
        if (audioSource != null && destroySound != null)
        {
            audioSource.Play();
        }

        // Destroy the object after the sound has played
        Destroy(gameObject, audioSource.clip.length); // Delay destruction by the sound duration
    }
}

