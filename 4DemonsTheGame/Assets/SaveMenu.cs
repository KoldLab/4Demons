using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveMenu : MonoBehaviour
{

    [Header("Unity Stuff")]
    public Player player;
    public SceneFade sceneFader;

    [Space]
    [Header("First Save")]
    public TextMeshProUGUI playerLvlReached1;
    public TextMeshProUGUI playerTimePlayed1;
    [Space]
    [Header("Second Save")]
    public TextMeshProUGUI playerLvlReached2;
    public TextMeshProUGUI playerTimePlayed2;
    [Space]
    [Header("Third Save")]
    public TextMeshProUGUI playerLvlReached3;
    public TextMeshProUGUI playerTimePlayed3;


    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;


        for (int i = 0; i < 3; i++)
        {
            if(i == 0)
            {
                PlayerData temporaryData = GetDataOfSaveSelected("FirstSave");
                playerLvlReached1.text = temporaryData.level.ToString();
                playerTimePlayed1.text = temporaryData.enemyKilled.ToString();
            }
            if (i == 1)
            {
                PlayerData temporaryData = GetDataOfSaveSelected("SecondSave");
                playerLvlReached2.text = temporaryData.level.ToString();
                playerTimePlayed2.text = temporaryData.enemyKilled.ToString();
            }
            if (i == 2)
            {
                PlayerData temporaryData = GetDataOfSaveSelected("ThirdSave");
                playerLvlReached3.text = temporaryData.level.ToString();
                playerTimePlayed3.text = temporaryData.enemyKilled.ToString();
            }

        }
        
    }

    public void goToScene(string sceneName)
    {
        sceneFader.FadeTo(sceneName);
    }

    public void Load(string savedFile)
    {
        player.Load(savedFile);
    }

    public PlayerData GetDataOfSaveSelected(string saveSelected)
    {
        string path = Application.persistentDataPath + "/" + saveSelected + ".sav"; // we are going to search the data where we saved it
        if (File.Exists(path)) // we look if the path exist
        {
            Debug.Log("File exists");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open); // we want to open the file so we can read it 

            PlayerData data = formatter.Deserialize(stream) as PlayerData; // we format the data into playerdata
            stream.Close();

            return data;
        }
        else //if not
        {
            return new PlayerData(new Player(), saveSelected);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
