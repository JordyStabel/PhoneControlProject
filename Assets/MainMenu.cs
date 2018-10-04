using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour { 
    public void PlayGame()
    {
        PlayerPrefs.SetInt("currentLevel", 0);
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        PlayerPrefs.SetInt("currentLevel", 0);
        Application.Quit();
    }
}
