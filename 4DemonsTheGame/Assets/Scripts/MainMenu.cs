using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject shopUI;

    public GameObject ui;

    public string mainMenuName = "Menu";

    public string levelOne = "WorldOne";

    public SceneFade sceneFader;
    

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {         
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ToggleShop()
    {
        Button[] buttons = shopUI.GetComponentsInChildren<Button>();
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = !buttons[i].interactable;
        }             
    }

    public void PlayGame()
    {
        sceneFader.FadeTo(levelOne);
    }
    public void Options()
    {
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    //gameMenu
    public void BackToMenu()
    {
        Toggle();
        sceneFader.FadeTo(mainMenuName);
    }
   
    public void Retry()
    {
        Debug.Log("Retry");
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
}
