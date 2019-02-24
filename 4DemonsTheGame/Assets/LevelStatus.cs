using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelStatus : MonoBehaviour
{
    public static int Money;
    public static int LifePoint;
    public int startLifePoint = 20;
    public int startMoney = 400;
    public GameObject soulsAmount;
    public GameObject hP;

    void Start()
    {
        LifePoint = startLifePoint;
        Money = startMoney;
    }
    void Update()
    {
        hP.GetComponent<TextMeshProUGUI>().text = LifePoint.ToString();
        soulsAmount.GetComponent<TextMeshProUGUI>().text = Money.ToString();
       
    }
}
