﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace Platformer.UI
{
    /// <summary>
    /// A simple controller for switching between UI panels.
    /// </summary>
    public class MainUIController : MonoBehaviour
    {
        public Button resumeBtn;
        public Button mainMenuBtn;

        public MetaGameController metaGameController;

        // public GameObject[] panels;

        // public void SetActivePanel(int index)
        // {
        //     for (var i = 0; i < panels.Length; i++)
        //     {
        //         var active = i == index;
        //         var g = panels[i];
        //         if (g.activeSelf != active) g.SetActive(active);
        //     }
        // }

        // void OnEnable()
        // {
            
        // }

        void Start()
        {
            metaGameController = FindObjectOfType<MetaGameController>();
            if(!metaGameController)
            {
                metaGameController = gameObject.AddComponent<MetaGameController>();
            }
            resumeBtn.onClick.AddListener(delegate{metaGameController.ToggleMainMenu(false);});
            mainMenuBtn.onClick.AddListener(delegate{SceneManager.LoadScene(0);});

        }
    }
}