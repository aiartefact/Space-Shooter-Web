using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupMenu : MonoBehaviour
{
    public Text titleText;
    public Text subTitleText;
    public Text warningText;
    public Text responsibilityText;
    public Text descriptionText;
    public Text rulesText;
    public Text controlsText;
    public Text aboutText;

    public Button startButton;
    public Button aboutButton;
    public Button backButton;

    public RawImage descriptionBackgroundImage;
    public RawImage rulesBackgroundImage;
    public RawImage controlsBackgroundImage;
    public RawImage aboutBackgroundImage;

    public GameObject player;

    public void GameLaunch()
    {
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
        StartupMenuOn();
    }

    private void StartupMenuOn()
    {
        player.SetActive(true);
        titleText.gameObject.SetActive(true);
        subTitleText.gameObject.SetActive(true);
        warningText.gameObject.SetActive(true);
        responsibilityText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        aboutButton.gameObject.SetActive(true);
    }
    private void StartupMenuOff()
    {
        player.SetActive(false);
        titleText.gameObject.SetActive(false);
        subTitleText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(false);
        responsibilityText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        aboutButton.gameObject.SetActive(false);
    }
    private void AboutMenuOn()
    {
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
}
