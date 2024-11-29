using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main; 
    public Transform StartingPoint;
    public Transform[] path;  // Corrected "transform" to "Transform"

    private void Awake()
    {
        main = this;
    }
}
