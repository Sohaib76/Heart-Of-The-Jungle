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

    public AudioSource buttonClickSound; // Reference to the AudioSource for button click sound


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
        // Play the button click sound
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }

        Debug.Log("Application closing");
        Application.Quit();
    }

    private void PlayButtonClicked()
    {

        // Play the button click sound
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }
        Debug.Log("Game starting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
