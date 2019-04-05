using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesLeft;
    public static int NumberOfRounds;

    public Wave[] waves;

    public Transform spawnPoint;
    public double timeBetweenWaves = 6f;
    private double countdown = 10;
    private int waveIndex = 0;
    public GameObject waveCountdownText;

    [HideInInspector]
    public float waitInBetweenEnemies = 0.5f;

    public GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        NumberOfRounds = waves.Length;
        EnemiesLeft = 0;
    }


    // Update is called once per frame
    void Update()
    {
        NumberOfRounds = waves.Length;

        if (GameController.GameIsOver){
            return;
        }
        if (EnemiesLeft > 0)
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

        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.GetAmountOfDifferentEnnemies(); i++)
        {
            wave.count += wave.counts[i];
        }
        EnemiesLeft = wave.count;
        for (int i = 0; i < wave.GetAmountOfDifferentEnnemies(); i++)
        {
            for (int j = 0; j < wave.counts[i]; j++)
            {
                SpawnEnemy(wave.enemies[i]);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        waveIndex++; //wave over
        if (LevelStatus.InfinityWave)
        {
            AddWaves();
        }

        if (waveIndex == waves.Length)
        {
            while (EnemiesLeft != 0)
            {
                yield return null;
            }

            gameController.WinLevel();
            this.enabled = false;


        }
        LevelStatus.Rounds++;
        LevelStatus.Money += 100;
    }
        void AddWaves()
        {
            int oldLenght = waves.Length;
            Array.Resize<Wave>(ref waves, waves.Length + 1);
            waves[oldLenght] = new Wave(waves[oldLenght - 1].enemies, waves[oldLenght - 1].counts, 1.2, waves[oldLenght - 1].rate);
        }

        void SpawnEnemy(GameObject enemy)
        {
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

        }
    
}
