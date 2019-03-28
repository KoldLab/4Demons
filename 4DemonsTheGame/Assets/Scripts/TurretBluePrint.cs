using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;


    // Start is called before the first frame update
    public int GetSellAmount()
    {
        return (int)(cost * 0.8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}