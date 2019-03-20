using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectsTable
{
    public GameObject[] towersPrefab;

    public GameObjectsTable(GameObject[] towersPrefab)
    {
        this.towersPrefab = towersPrefab;
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
