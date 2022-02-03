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
    public Text messageText;
    public Button restartGameOverButton;
    public Button launchMenuGameOverButton;
    public Button continuePauseButton;
    public Button restartPauseButton;
    public Button launchMenuPauseButton;

    private bool gameOver;
    private int score;

    // New Scene for the restart
    Scene scene;

    // Screen resolution change UI fix
    private int PrevScreenWidth = 1366;
    private int PrevScreenHeight = 768;

    // waveCount for waveWait change
    private int waveCount;

    // Movement speed increment
    public PlayerController player;
    private GameObject tempObject;
    private Mover mover;
    private float speedIncrement;
    public int speedIncrementsEveryNthWave;
    public int waveWaitDecrementsEveryNthWave;
    public float waveWaitDecrementStep; // Step for decrement of the delay before the next wave
    public float playerSpeedLimit; // Player speed value that causes significant speed increment decrease
    public float speedIncrementStepStart; // Step for speed increment until Player speed reaches playerSpeedLimit
    public float speedIncrementStepEnd; // Step for speed increment after Player speed reaches playerSpeedLimit
    public float playerSpeedStopLimit; // Player speed value that causes speed increment procedure to stop
    public float spawnWaitLimit; // Limit value that causes spawn wait decrement procedure to stop
    private float spawnWaitStepStart;
    private float spawnWaitStepEnd;

    // Fire rate increment
    private WeaponController weapon;
    private float fireRateIncrement;
    private float enemyFireDelayDecrement;
    public float fireRateIncrementStepStart; // Step for fire rate increment until Player speed reaches playerSpeedLimit
    public float fireRateIncrementStepEnd; // Step for fire rate increment after Player speed reaches playerSpeedLimit
    public float enemyFireRateIncrementCoef; // Coefficient for enemy fire rate increment in comparison to the Player's one
    public float enemyFireDelayDecrementStepStart; // Step for enemy fire delay decrement until Player speed reaches playerSpeedLimit
    public float enemyFireDelayDecrementStepEnd; // Step for enemy fire delay decrement after Player speed reaches playerSpeedLimit

    // Shot speed increment
    private float shotSpeedIncrement;
    public float shotSpeedIncrementStepStart; // Step for shot speed increment until Player speed reaches playerSpeedLimit
    public float shotSpeedIncrementStepEnd; // Step for shot speed increment after Player speed reaches playerSpeedLimit

    private void Start()
    {
        gameOver = false;
        messageText.text = "";
        ButtonsGameOverOff();
        ButtonsPauseOff();
        score = 0;
        // If the game was paused in the previous scene then unpause it
        if (PauseManager.pauseManager.gamePaused)
        {
            PauseManager.pauseManager.PauseGame();
        }
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
        // spawnWaitStep initialization
        spawnWaitStepStart = speedIncrementStepStart * 0.05f;
        spawnWaitStepEnd = speedIncrementStepEnd * 0.05f;
        // fireRateIncrement initialization
        fireRateIncrement = 0;
        // enemyFireDelayDecrement initialization
        enemyFireDelayDecrement = 0;
        // shotSpeedIncrement initialization
        shotSpeedIncrement = 0;

        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    private void Update()
    {
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

        // Check for user input for pause if the game is not over
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.P)) && !gameOver)
        {
            PauseGame();
        }
    } 

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            // TODO: Difficulty levels
            // TODO: Highscore (optional)
            
            // waveCount increment
            waveCount++;
            // Movement speed increment every speedIncrementsEveryNthWave-th wave IF stop limit of Player speed is not reached
            if ((player.speed < playerSpeedStopLimit) && (waveCount % speedIncrementsEveryNthWave == 0))
            {
                if (player.speed < playerSpeedLimit)
                {
                    speedIncrement += speedIncrementStepStart;
                    fireRateIncrement += fireRateIncrementStepStart * enemyFireRateIncrementCoef;
                    enemyFireDelayDecrement += enemyFireDelayDecrementStepStart;
                    shotSpeedIncrement += shotSpeedIncrementStepStart;
                    // Player movement speed increment
                    player.speed += speedIncrementStepStart;
                    // Player fire rate increment
                    player.fireRate -= fireRateIncrementStepStart;
                    // Player shot speed increment
                    player.ShotSpeedIncrement(shotSpeedIncrementStepStart);
                    // Decrease the delay before the next hazard until it reaches spawnWaitLimit
                    if (spawnWait > spawnWaitLimit)
                    {
                        spawnWait -= spawnWaitStepStart; // 0.05s for 1 speed unit
                    }
                }
                else
                {
                    speedIncrement += speedIncrementStepEnd;
                    fireRateIncrement += fireRateIncrementStepEnd * enemyFireRateIncrementCoef;
                    enemyFireDelayDecrement += enemyFireDelayDecrementStepEnd;
                    shotSpeedIncrement += shotSpeedIncrementStepEnd;
                    player.speed += speedIncrementStepEnd;
                    player.fireRate -= fireRateIncrementStepEnd;
                    player.ShotSpeedIncrement(shotSpeedIncrementStepEnd);
                    if (spawnWait > spawnWaitLimit)
                    {
                        spawnWait -= spawnWaitStepEnd;
                    }
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

                if (weapon = tempObject.GetComponent<WeaponController>())
                {
                    // Enemy fire rate increment
                    weapon.fireRate -= fireRateIncrement;
                    weapon.delay -= enemyFireDelayDecrement;
                    // Enemy shot speed increment
                    weapon.ShotSpeedIncrement(shotSpeedIncrement);
                }

                yield return new WaitForSeconds(spawnWait);
            }

            // Decrease the delay before the next wave every waveWaitDecrementsEveryNthWave-th wave until it reaches 0
            if (waveWait > 0.1f) // 0.1f is used to avoid comparison float to 0
            {
                if (waveCount % waveWaitDecrementsEveryNthWave == 0)
                {
                    waveWait -= waveWaitDecrementStep;                
                }
                yield return new WaitForSeconds(waveWait);
            }
            //yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
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
        messageText.text = "Game Over!";
        ButtonsGameOverOn();
        gameOver = true;
    }

    private void ButtonsGameOverOff()
    {
        restartGameOverButton.gameObject.SetActive(false);
        launchMenuGameOverButton.gameObject.SetActive(false);
    }
    private void ButtonsGameOverOn()
    {
        restartGameOverButton.gameObject.SetActive(true);
        launchMenuGameOverButton.gameObject.SetActive(true);
    }
    private void ButtonsPauseOff()
    {
        restartPauseButton.gameObject.SetActive(false);
        launchMenuPauseButton.gameObject.SetActive(false);
        continuePauseButton.gameObject.SetActive(false);
    }
    private void ButtonsPauseOn()
    {
        restartPauseButton.gameObject.SetActive(true);
        launchMenuPauseButton.gameObject.SetActive(true);
        continuePauseButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(scene.name);
    }
    public void ReturnToLaunchMenu()
    {
        SceneManager.LoadSceneAsync("_Scenes/StartupMenu");
    }

    // Sets and unsets the Pause state
    private void PauseMenuSwitch()
    {
        if (PauseManager.pauseManager.gamePaused)
        {
            messageText.text = "Game paused";
            ButtonsPauseOn();
        }
        else
        {
            messageText.text = "";
            ButtonsPauseOff();
        }
    }
    public void PauseGame()
    {
        PauseManager.pauseManager.PauseGame();
        PauseMenuSwitch();
    }
}
