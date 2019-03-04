using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;
    public bool waveStatus = false; //if wave started = true
    public Wave[] waves;

    public Transform spawnPoint;
    public double timeBetweenWaves = 6f;
    private double countdown = 2;
    private int waveIndex = 0;

    [HideInInspector]
    public float waitInBetweenEnemies = 0.5f;

    public GameObject waveCountdownText;

    // Start is called before the first frame update
    void Start()
    {
         EnemiesAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(waveStatus == true)
        {
            return;
        }
        if(EnemiesAlive > 0)
        {                      
            return;
        }
        if (countdown <= 0)//quand le countdown est 0 on spawn ennemies
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
        if(countdown < 0)
        {
            waveCountdownText.GetComponent<TextMeshProUGUI>().text = "0.00";
        }
        else
            waveCountdownText.GetComponent<TextMeshProUGUI>().text = countdown.ToString("f2");
     
    }

    IEnumerator SpawnWave()
    {
        waveStatus = true;
        LevelStatus.Rounds++;

        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;

        if(waveIndex == waves.Length)
        {
            GameController.GameIsOver = true;
            this.enabled = false;
        }
        waveStatus = false;
    }

    void SpawnEnemy(GameObject enemy)
    {
        
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        
        EnemiesAlive++;
    }

}
