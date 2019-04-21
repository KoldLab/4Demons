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
    public TimeControl timeController;
    public CameraController cam;
    public GameObject demon;

    public string mainMenuName = "Menu";

    void Awake()
    {
        //each time we start the game there's only one GameManager and this variable can be accessed by anywhere
        if (instance != null)
        {
            Debug.LogError("More than one GameController in scene");
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
        timeController = TimeControl.instance;
        GameIsOver = false;
        demon = (GameObject)Instantiate(demon, new Vector2(spawnPoint.position.x, spawnPoint.position.y), spawnPoint.rotation);
        demon.name = "Demon";
        cam.FollowPlayer();
        StartCoroutine(autoSave());
    }

    IEnumerator autoSave()
    {
        for (; ; )
        {
            player.Save(player.currentSavedFile);            //sauvegarde toute les 5 secondtes
            yield return new WaitForSecondsRealtime(5f);
        }
        
    }

    void Save()
    {
        player.Save(player.currentSavedFile);
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
        timeController.Pause();
    }

    public void Resume()
    {
        timeController.Resume();
    }

    public void WinLevel()
    {
        levelWonUI.SetActive(true);
        GameIsOver = true;
    }
}
