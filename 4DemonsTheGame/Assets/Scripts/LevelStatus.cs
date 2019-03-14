using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelStatus : MonoBehaviour
{
    public static int Money;
    public static int LifePoint;
    public static int Rounds;
    public static int EnemyKilled;
    public static bool InfinityWave = false;
    public int startLifePoint = 20;
    public int startMoney = 400;
    public bool infinityWaves;
    public GameObject soulsAmount;
    public GameObject hP;
    public GameObject waves;

    void Start()
    {
        InfinityWave = infinityWaves;
        LifePoint = startLifePoint;
        Money = startMoney;
        Rounds = 0; //il faut redefinir car variable static transporte dans toutes les scenes
        EnemyKilled = 0;
    }
    void Update()
    {
        hP.GetComponent<TextMeshProUGUI>().text = LifePoint.ToString();
        soulsAmount.GetComponent<TextMeshProUGUI>().text = Money.ToString();
        if (InfinityWave)
        {
            waves.GetComponent<TextMeshProUGUI>().text = Rounds.ToString();
        }
        else
        {
            waves.GetComponent<TextMeshProUGUI>().text = Rounds.ToString() + "/" + WaveSpawner.NumberOfRounds.ToString();
        }
        

    }
}
