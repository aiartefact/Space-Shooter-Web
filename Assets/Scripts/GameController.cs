using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    // New Scene for the restart
    Scene scene;

    // Screen resolution change UI fix
    private int PrevScreenWidth = 1366;
    private int PrevScreenHeight = 768;
    private Vector3 PrevScoreTextPos = new Vector3(398,768,0);

    // waveCount for waveWait change
    private int waveCount;

    // Movement speed increment
    public PlayerController player;
    private GameObject tempObject;
    private Mover mover;
    private float speedIncrement;
    private void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        // Screen resolution change UI fix
        if (Screen.width != PrevScreenWidth)
        {
            PrevScreenWidth = Screen.width;
            PrevScreenHeight = Screen.height;
            //scoreText.rectTransform.position = new Vector3(Screen.width * 0.5f - Screen.width * 0.41f * 0.5f, Screen.height, 0);
            scoreText.rectTransform.position = new Vector3(Screen.width * 0.295f, Screen.height, 0);
        }
        else if (Screen.height != PrevScreenHeight)
        {
            PrevScreenWidth = Screen.width;
            PrevScreenHeight = Screen.height;
            scoreText.rectTransform.position = new Vector3(Screen.width * 0.295f, Screen.height, 0);
        }
        // waveCount initialization
        waveCount = 0;
        // speedIncrement initialization
        speedIncrement = 0;

        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    private void Update()
    {
        //Debug.Log(scoreText.rectTransform.position);
        //Debug.Log(Screen.width + "x" + Screen.height);
        // Screen resolution change UI fix
        if (Screen.width != PrevScreenWidth)
        {
            PrevScreenWidth = Screen.width;
            PrevScreenHeight = Screen.height;
            scoreText.rectTransform.position = new Vector3(Screen.width * 0.295f, Screen.height, 0);
        }
        else if (Screen.height != PrevScreenHeight)
        {
            PrevScreenWidth = Screen.width;
            PrevScreenHeight = Screen.height;
            scoreText.rectTransform.position = new Vector3(Screen.width * 0.295f, Screen.height, 0);
        }
        // Restart
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                scene = SceneManager.GetActiveScene();
                SceneManager.LoadSceneAsync(scene.name);
                //Application.LoadLevel(Application.loadedLevel);
            }
        }
    } 

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            // TODO: Increment the bolt speed and fire rate for both player and enemies
            // TODO: Intro with game description, rules and controls
            
            // waveCount increment
            waveCount++;
            // Movement speed increment every 4th wave
            if (waveCount % 4 == 0)
            {
                speedIncrement += 0.5f;
                // Player movement speed increment
                player.speed += 0.5f;
                // Decrease the delay before the next hazard every 8th wave until it reaches 0.05
                if ((spawnWait > 0.11f) && (waveCount % 8 == 0)) // 0.11f is used to avoid comparison float to 0.1
                {
                    spawnWait -= 0.05f; // 0.05s for 1 speed unit
                }
            }

            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                //Instantiate(hazard, spawnPosition, spawnRotation);
                tempObject = Instantiate(hazard, spawnPosition, spawnRotation);

                // Movement speed increment 
                mover = tempObject.GetComponent<Mover>();
                mover.speed -= speedIncrement; // Hazard speed is negative for transform purposes

                yield return new WaitForSeconds(spawnWait);
            }

            // Decrease the delay before the next wave every 3rd wave until it reaches 0
            if (waveWait > 0.1f) // 0.1f is used to avoid comparison float to 0
            {
                if (waveCount % 3 == 0)
                {
                    waveWait -= 0.5f;                
                }
                yield return new WaitForSeconds(waveWait);
            }
            //yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }
	
    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
