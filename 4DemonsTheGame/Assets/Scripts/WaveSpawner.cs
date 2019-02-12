using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform enemyPrefab2;
    public Transform spawnPoint;

    public float timeBetweenWaves = 6f;
    private float countdown = 2;
    private int waveIndex = 0;
    public float waitInBetweenEnemies = 0.5f;
    public GameObject waveCountdownText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0)//quand le countdown est 0 on spawn ennemies
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
       waveCountdownText.GetComponent<TextMeshProUGUI>().text = countdown.ToString("f2");
     
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waitInBetweenEnemies);
        }

    }

    void SpawnEnemy()
    {
        if(waveIndex % 2 == 0)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Instantiate(enemyPrefab2, spawnPoint.position, spawnPoint.rotation);
        }
    }

}
