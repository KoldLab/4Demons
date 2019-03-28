using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 1;
    public int timeLessSouls = 0;

    public void Save()
    {
        SaveSystem.SavePlayer(this);
    }
    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        timeLessSouls = data.timeLessSouls;       
    }
    public Player()
    {
        level = 1;
        timeLessSouls = 0;
    }
}
