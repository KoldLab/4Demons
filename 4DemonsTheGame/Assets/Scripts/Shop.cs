using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTower;
    public TurretBlueprint missileTower;

    public TextMeshProUGUI standardTowerCost;
    public TextMeshProUGUI missileTowerCost;

    BuildManager buildManager;

    public void SelectTurretOne()
    {
        if(buildManager.getTurretToBuild() == standardTower)
        {
            EventSystem.current.SetSelectedGameObject(null);
            buildManager.cancelBuild();           
            return;
        }
        buildManager.SelectTurretToBuild(standardTower);
    }
    public void SelectTurretTwo()
    {
        if (buildManager.getTurretToBuild() == missileTower)
        {
            EventSystem.current.SetSelectedGameObject(null);
            buildManager.cancelBuild();
            return;
        }
        buildManager.SelectTurretToBuild(missileTower);
    }

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        standardTowerCost.text = standardTower.cost + "$";
        missileTowerCost.text = missileTower.cost + "$";
    }


    // Update is called once per frame
}
