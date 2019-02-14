using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTower;

    BuildManager buildManager;

    public void SelectTurretOne()
    {
        buildManager.SelectTurretToBuild(standardTower);
        
    }

    //public void SelectTurretTwo()
    //{
    //    buildManager.SelectTurretToBuild(buildManager.TurretTwoPrefab);
    //}

    //public void SelectTurretThree()
    //{
    //    buildManager.SelectTurretToBuild(buildManager.TurretThreePrefab);
    //}
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
