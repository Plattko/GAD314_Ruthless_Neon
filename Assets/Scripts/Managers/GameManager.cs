using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RectTransform pauseMenu;
    [SerializeField] private RectTransform deathMenu;
    public static GameManager instance;
    private bool isGamePaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        pauseMenu.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        if (!isGamePaused)
        {
            EnterPauseMenu();
        }
        else
        {
            ExitPauseMenu();
        }
    }

    public void EnterPauseMenu()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    public void ExitPauseMenu()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }

    public void EnterDeathMenu()
    {
        Time.timeScale = 0;
        deathMenu.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
