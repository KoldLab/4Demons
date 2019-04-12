using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFade sceneFader;
    public GameObject uiCanvas;
    public Player player;

    public string mainMenuName = "Menu";

    public string nextLevel = "WorldTwo";
    public int levelToUnlock = 2;
    // Start is called before the first frame update


    private void Start()
    {
        player = Player.Instance;
    }
    public void Continue()
    {
        player.level = levelToUnlock;
        player.Save(player.GetCurrentSavedFile());
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
