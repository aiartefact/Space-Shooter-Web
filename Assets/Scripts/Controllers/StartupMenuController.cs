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

    public Button startButton;
    public Button aboutButton;
    public Button backButton;
    public Button easyDifficultyButton;
    public Button mediumDifficultyButton;
    public Button hardDifficultyButton;
    public Button veryHardDifficultyButton;

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
    public void Back()
    {
        AboutMenuOff();
        DifficultyMenuOff();
        StartupMenuOn();
    }
    public void SelectDifficulty()
    {
        StartupMenuOff();
        DifficultyMenuOn();
    }

    private void StartupMenuOn()
    {
        titleText.gameObject.SetActive(true);
        subTitleText.gameObject.SetActive(true);
        warningText.gameObject.SetActive(true);
        responsibilityText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        aboutButton.gameObject.SetActive(true);
    }
    private void StartupMenuOff()
    {
        titleText.gameObject.SetActive(false);
        subTitleText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(false);
        responsibilityText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
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
        backButton.gameObject.SetActive(true);
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
        backButton.gameObject.SetActive(false);
    }
    private void DifficultyMenuOn()
    {
        selectDifficultyText.gameObject.SetActive(true);
        easyDifficultyButton.gameObject.SetActive(true);
        mediumDifficultyButton.gameObject.SetActive(true);
        hardDifficultyButton.gameObject.SetActive(true);
        veryHardDifficultyButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }
    private void DifficultyMenuOff()
    {
        selectDifficultyText.gameObject.SetActive(false);
        easyDifficultyButton.gameObject.SetActive(false);
        mediumDifficultyButton.gameObject.SetActive(false);
        hardDifficultyButton.gameObject.SetActive(false);
        veryHardDifficultyButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
    }
}
