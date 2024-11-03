using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void LoadAlleyway()
    {
        SceneManager.LoadScene("AlleywayLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
