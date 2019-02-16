using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTower;
    public TurretBlueprint missileTower;

    BuildManager buildManager;

    public void SelectTurretOne()
    {
        buildManager.gameController.Pause();
        Debug.Log(buildManager.getActive());
        if (buildManager.getActive() == true)
        {
            buildManager.cancelBuild();
            buildManager.setActive(false);
            return;
        }
        buildManager.setActive(true);
        buildManager.SelectTurretToBuild(standardTower);
    }
    public void SelectTurretTwo()
    {
        buildManager.gameController.Pause();
        Debug.Log(buildManager.getActive());
        if (buildManager.getActive() == true)
        {
            buildManager.cancelBuild();
            buildManager.setActive(false);
            return;
        }
        buildManager.setActive(true);
        buildManager.SelectTurretToBuild(missileTower);
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
