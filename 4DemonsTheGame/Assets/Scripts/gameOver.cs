using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{

    public TextMeshProUGUI enemiesKilled;
    public TextMeshProUGUI waves;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waves.text = LevelStatus.Rounds.ToString();
        enemiesKilled.text = LevelStatus.EnemyKilled.ToString();
    }
}
