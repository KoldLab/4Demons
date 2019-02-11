using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStatus : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;
    public GameObject soulsAmount;

    void Start()
    {
        Money = startMoney;
    }
    void Update()
    {
        soulsAmount.GetComponent<TextMeshProUGUI>().text = Money.ToString();
    }
}
