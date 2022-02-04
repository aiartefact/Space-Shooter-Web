using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager difficultyManager;

    public int difficultyLevel = 0;

    void Awake()
    {
        if (difficultyManager == null)
        {
            DontDestroyOnLoad(gameObject);
            difficultyManager = this;            
        }
        else if (difficultyManager != this)
        {
            Destroy(gameObject);
        }
    }

}
