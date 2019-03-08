using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{

    public SceneFade sceneFader;
    public string mainMenuName = "Menu";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Retry()
    {
        Time.timeScale = 1;
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    //gameMenu
    public void BackToMenu()
    {
        Time.timeScale = 1;
        sceneFader.FadeTo(mainMenuName);
    }
}
