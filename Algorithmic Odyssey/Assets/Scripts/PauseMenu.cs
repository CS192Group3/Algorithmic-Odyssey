using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsPanel;
    public GameObject tutPanel;
    public GameObject pauseButt;
    public bool isPaused;
    public bool inSettings;
    public static bool tutDone = true;
    private int currentSceneIndex;
    SavePlayerPos playerPosData;
    
    void Start()
    {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
        tutPanel.SetActive(tutDone);
        pauseButt.SetActive(!tutDone);
        playerPosData = FindObjectOfType<SavePlayerPos>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(true);
        pauseButt.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
        pauseButt.SetActive(true);
        tutPanel.SetActive(false);
        tutDone = false;
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        playerPosData.PlayerPosSave();
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }
    public void OpenSettings()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(true);
    }
}
