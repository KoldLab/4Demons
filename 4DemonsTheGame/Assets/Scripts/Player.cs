using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int level = 1;
    public int timeLessSouls = 0;
    public string currentSavedFile;
    public int enemyKilled = 0;

    public GameObject demon;

    public static Player Instance
    {
        get { return _instance ?? (_instance = new GameObject("Player").AddComponent<Player>()); }
    }

    private static Player _instance;

    private void Awake()
    {
        //If instance is already set and this script is not it, then destroy it.
        //We already have a MyClass in this instance.
        if (_instance != null && _instance != this)
            Debug.Log("destroyy");
            Destroy(gameObject);
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void Save(string savedFile)
    {
        Debug.Log("Game saved to :" + currentSavedFile);
        SaveSystem.SavePlayer(_instance, savedFile);
    }
    public void Load(string savedFile)
    {
        PlayerData data = SaveSystem.LoadPlayer(savedFile);
        _instance.level = data.level;
        _instance.timeLessSouls = data.timeLessSouls;
        _instance.currentSavedFile = savedFile;
    }

    public string GetCurrentSavedFile()
    {
        return currentSavedFile;
    }

    private void Update()
    {
        
    }
}
