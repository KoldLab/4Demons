using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFade sceneFader;
    public GameObject uiCanvas;

    public string mainMenuName = "Menu";

    public string nextLevel = "WorldTwo";
    public int levelToUnlock = 2;
    // Start is called before the first frame update

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        sceneFader.FadeTo(mainMenuName);
    }

    private void Update()
    {
        if(GameController.GameIsOver == true)
        {
            uiCanvas.SetActive(false);
        }
    }
}
