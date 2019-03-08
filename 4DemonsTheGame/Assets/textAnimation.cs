using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class textAnimation : MonoBehaviour
{

    public TextMeshProUGUI enemiesKilled;
    public TextMeshProUGUI waves;



    void OnEnable()
    {
        waves.text = LevelStatus.Rounds.ToString();       
    }
}
