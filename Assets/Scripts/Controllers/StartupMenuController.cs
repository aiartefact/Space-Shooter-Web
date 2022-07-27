using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupMenuController : MonoBehaviour
{
    public Text titleText;
    public Text subTitleText;
    public Text warningText;
    public Text responsibilityText;
    public Text descriptionText;
    public Text rulesText;
    public Text controlsText;
    public Text aboutText;
    public Text selectDifficultyText;
    public Text selectHighscoreText;
    public Text scoreText;

    public Button startButton;
    public Button aboutButton;
    public Button backFromAboutButton;
    public Button easyDifficultyButton;
    public Button mediumDifficultyButton;
    public Button hardDifficultyButton;
    public Button veryHardDifficultyButton;
    public Button backFromSelectDifficultyButton;
    public Button highscoreButton;
    public Button easyHighscoreButton;
    public Button mediumHighscoreButton;
    public Button hardHighscoreButton;
    public Button veryHardHighscoreButton;
    public Button backFromHighscoreButton;
    public Button backFromScoreButton;

    public RawImage descriptionBackgroundImage;
    public RawImage rulesBackgroundImage;
    public RawImage controlsBackgroundImage;
    public RawImage aboutBackgroundImage;

    public GameObject player;

    private void Start()
    {
        // If the game was paused in the previous scene then unpause it
        if (PauseManager.pauseManager.gamePaused)
        {
            PauseManager.pauseManager.PauseGame();
        }
    }

    public void GameLaunch(int difficultyLevel)
    {
        DifficultyManager.difficultyManager.difficultyLevel = difficultyLevel;
        SceneManager.LoadSceneAsync("_Scenes/Main");
    }

    public void About()
    {
        StartupMenuOff();
        AboutMenuOn();
    }
    public void BackFromAbout()
    {
        AboutMenuOff();
        StartupMenuOn();
    }
    public void SelectDifficulty()
    {
        StartupMenuOff();
        DifficultyMenuOn();
    }
    public void BackFromSelectDifficulty()
    {
        DifficultyMenuOff();
        StartupMenuOn();
    }
    public void Highscore()
    {
        StartupMenuOff();
        HighscoreMenuOn();
    }
    public void BackFromHighscore()
    {
        HighscoreMenuOff();
        StartupMenuOn();        
    }
    public void LoadScore(string difficulty)
    {
        HighscoreMenuOff();

    }
    public void BackFromScore()
    {

        HighscoreMenuOn();
    }

    private void StartupMenuOn()
    {
        titleText.gameObject.SetActive(true);
        subTitleText.gameObject.SetActive(true);
        warningText.gameObject.SetActive(true);
        responsibilityText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        highscoreButton.gameObject.SetActive(true);
        aboutButton.gameObject.SetActive(true);
    }
    private void StartupMenuOff()
    {
        titleText.gameObject.SetActive(false);
        subTitleText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(false);
        responsibilityText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        highscoreButton.gameObject.SetActive(false);
        aboutButton.gameObject.SetActive(false);
    }
    private void AboutMenuOn()
    {
        player.SetActive(false);
        descriptionBackgroundImage.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);
        rulesBackgroundImage.gameObject.SetActive(true);
        rulesText.gameObject.SetActive(true);
        controlsBackgroundImage.gameObject.SetActive(true);
        controlsText.gameObject.SetActive(true);
        aboutBackgroundImage.gameObject.SetActive(true);
        aboutText.gameObject.SetActive(true);
        backFromAboutButton.gameObject.SetActive(true);
    }
    private void AboutMenuOff()
    {
        player.SetActive(true);
        descriptionBackgroundImage.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
        rulesBackgroundImage.gameObject.SetActive(false);
        rulesText.gameObject.SetActive(false);
        controlsBackgroundImage.gameObject.SetActive(false);
        controlsText.gameObject.SetActive(false);
        aboutBackgroundImage.gameObject.SetActive(false);
        aboutText.gameObject.SetActive(false);
        backFromAboutButton.gameObject.SetActive(false);
    }
    private void DifficultyMenuOn()
    {
        selectDifficultyText.gameObject.SetActive(true);
        easyDifficultyButton.gameObject.SetActive(true);
        mediumDifficultyButton.gameObject.SetActive(true);
        hardDifficultyButton.gameObject.SetActive(true);
        veryHardDifficultyButton.gameObject.SetActive(true);
        backFromSelectDifficultyButton.gameObject.SetActive(true);
    }
    private void DifficultyMenuOff()
    {
        selectDifficultyText.gameObject.SetActive(false);
        easyDifficultyButton.gameObject.SetActive(false);
        mediumDifficultyButton.gameObject.SetActive(false);
        hardDifficultyButton.gameObject.SetActive(false);
        veryHardDifficultyButton.gameObject.SetActive(false);
        backFromSelectDifficultyButton.gameObject.SetActive(false);
    }
    private void HighscoreMenuOn()
    {
        selectHighscoreText.gameObject.SetActive(true);
        easyHighscoreButton.gameObject.SetActive(true);
        mediumHighscoreButton.gameObject.SetActive(true);
        hardHighscoreButton.gameObject.SetActive(true);
        veryHardHighscoreButton.gameObject.SetActive(true);
        backFromHighscoreButton.gameObject.SetActive(true);
    }
    private void HighscoreMenuOff()
    {
        selectHighscoreText.gameObject.SetActive(false);
        easyHighscoreButton.gameObject.SetActive(false);
        mediumHighscoreButton.gameObject.SetActive(false);
        hardHighscoreButton.gameObject.SetActive(false);
        veryHardHighscoreButton.gameObject.SetActive(false);
        backFromHighscoreButton.gameObject.SetActive(false);
    }
    private void ScoreMenuOn()
    {
        scoreText.gameObject.SetActive(true);        
        backFromScoreButton.gameObject.SetActive(true);
    }
}
