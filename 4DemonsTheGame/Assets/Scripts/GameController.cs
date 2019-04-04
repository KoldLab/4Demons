using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject levelWonUI;

    public Transform spawnPoint;

    public Player player;
    public CameraController cam;
    public GameObject demon;

    public string mainMenuName = "Menu";

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
        demon = (GameObject)Instantiate(player.demon, new Vector2(spawnPoint.position.x, spawnPoint.position.y), spawnPoint.rotation);
        demon.name = "Demon";
        cam.FollowPlayer();
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
            EndGame();
        }
        if (Demon.IsDead)
        {
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

    public void WinLevel()
    {
        levelWonUI.SetActive(true);
        GameIsOver = true;
    }
}
