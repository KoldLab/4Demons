using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static bool GameIsOver;

    public GameObject gameOverUI;

    public string mainMenuName = "Menu";

    public SceneFade scenefader;

    void Awake()
    {
        //each time we start the game there's only one buildmanager and this variable can be accessed by anywhere
        if (instance != null)
        {
            Debug.LogError("More than one GameController in scene");
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
        {
            
            return;
        }
        if(LevelStatus.LifePoint <= 0)
        {
            Debug.Log("Rounds survivded" + LevelStatus.Rounds);
            EndGame();
        }
    }

    public void EndGame()
    {
        GameIsOver = true;
        Pause();
        gameOverUI.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
