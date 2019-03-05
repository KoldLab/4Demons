using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public int enemiesAlive;
    public static int EnemiesAlive;
    public static int EnemiesLeft;
    public static int NumberOfRounds;
    public static bool LastWaveIsOVer;
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
        NumberOfRounds = waves.Length;
        LastWaveIsOVer = false;
}

    // Update is called once per frame
    void Update()
    {
        enemiesAlive = EnemiesLeft;
        if (waveStatus == true)
        {
            return;
        }
        if(EnemiesLeft > 0)
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

    IEnumerator SpawnWave() //spawn a new wave
    {
        waveStatus = true; //il y a une vague en ce moment

        Wave wave = waves[waveIndex];
        EnemiesLeft = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        Debug.Log("++waveindex");
        waveIndex++;

        if(waveIndex == waves.Length)
        {
            while (EnemiesLeft!=0)
            {
                yield return null;
            }

            LastWaveIsOVer = true;
            this.enabled = false;
            
        }
        waveStatus = false;
        LevelStatus.Rounds++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        
        EnemiesAlive++;
    }

}
