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

    public Button startButton;
    public Button aboutButton;

    public RawImage backgroundImage;

    // Screen resolution change UI fix
    private int PrevScreenWidth = 1366;
    private int PrevScreenHeight = 768;

    // Start is called before the first frame update
    void Start()
    {
        // Screen resolution change UI fix
        if (Screen.width != PrevScreenWidth)
        {
            PrevScreenWidth = Screen.width;
            PrevScreenHeight = Screen.height;
            //scoreText.rectTransform.position = new Vector3(Screen.width * 0.295f, Screen.height, 0);
        }
        else if (Screen.height != PrevScreenHeight)
        {
            PrevScreenWidth = Screen.width;
            PrevScreenHeight = Screen.height;
            //scoreText.rectTransform.position = new Vector3(Screen.width * 0.295f, Screen.height, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
