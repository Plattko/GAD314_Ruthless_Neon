using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RectTransform pauseMenu;
    public static GameManager instance;
    private bool isGamePaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        pauseMenu.gameObject.SetActive(false);
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
