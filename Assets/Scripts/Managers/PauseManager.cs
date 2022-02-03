using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager pauseManager;

    public bool gamePaused = false;
    
    void Awake()
    {
        if (pauseManager == null)
        {
            DontDestroyOnLoad(gameObject);
            pauseManager = this;
        }
        else if (pauseManager != this)
        {
            Destroy(gameObject);
        }
    }

    // Sets and unsets the Pause state
    public void PauseGame()
    {
        Time.timeScale = Time.timeScale == 0 ? 1f : 0f;
        AudioListener.pause = AudioListener.pause == false ? true : false;
        gamePaused = !gamePaused;
    }
}
