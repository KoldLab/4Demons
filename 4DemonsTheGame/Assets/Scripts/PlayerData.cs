using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData{

    public int level;
    public int timeLessSouls;

    public PlayerData(Player player)
    {
        level = player.level;
        timeLessSouls = player.timeLessSouls;
    }
}
