using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData{

    public int level;
    public int timeLessSouls;
    public int enemyKilled;
    public string savedFile;

    public PlayerData(Player player, string _savedFile)
    {
        level = player.level;
        timeLessSouls = player.timeLessSouls;
        savedFile = _savedFile;
        enemyKilled = player.enemyKilled;
    }
}
