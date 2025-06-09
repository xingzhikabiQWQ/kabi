using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemanu : MonoBehaviour
{
    public static Pausemanu instance;
    public GameObject pauseScreen;
    public bool isPaused;
    public string levelSelect, mainMenu;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            
        }
    }

    public void LevelSelect()
    {
		PlayerPrefs.SetString("CurrentLevel",SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1;
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1;
    }
}
