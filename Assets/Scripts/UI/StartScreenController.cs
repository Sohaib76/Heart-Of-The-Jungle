using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    private Button playButton;
    private Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton = transform.Find("Canvas/PlayButton").GetComponent<Button>();
        quitButton = transform.Find("Canvas/QuitButton").GetComponent<Button>();

        playButton.onClick.AddListener(PlayButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    private void QuitButtonClicked()
    {
        Debug.Log("Application closing");
        Application.Quit();
    }

    private void PlayButtonClicked()
    {
        Debug.Log("Game starting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
