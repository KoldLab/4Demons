using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    public GameObject[] enemies;
    [HideInInspector]
    public int count;
    public int[] counts;
    public float rate;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public int GetAmountOfDifferentEnnemies()
    {
        return enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
