using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Tower Blueprints")]
    public TurretBlueprint[] towersBlueprintTable = new TurretBlueprint[15];

    [Header("Tower's cost text")]
    public TextMeshProUGUI fireTowerCost;
    public TextMeshProUGUI windTowerCost;
    public TextMeshProUGUI lightningTowerCost;
    public TextMeshProUGUI earthTowerCost;
    public TextMeshProUGUI waterTowerCost;

    [Header("Tower Buttons")]
    public Button fireTowerButton;
    public Button windTowerButton;
    public Button lightningTowerButton;
    public Button earthTowerButton;
    public Button waterTowerButton;

    private Node target;

    BuildManager buildManager;

    public TurretBlueprint[] getTowersBlueprintTable()
    {
        return towersBlueprintTable;
    }

    public void SelectFireTower()
    {
        buildManager.BuildTurret(towersBlueprintTable[0], target);//when u click on the turret it buys it
                
    }
    public void SelectWindTower()
    {
        buildManager.BuildTurret(towersBlueprintTable[1], target);//when u click on the turret it buys it

    }
    public void SelectLightningTower()
    {
        buildManager.BuildTurret(towersBlueprintTable[2], target);//when u click on the turret it buys it

    }
    public void SelectEarthTower()
    {
        
        buildManager.BuildTurret(towersBlueprintTable[3], target);//when u click on the turret it buys it

    }
    public void SelectWaterTower()
    {
        buildManager.BuildTurret(towersBlueprintTable[4], target);//when u click on the turret it buys it

    }


    public void SetNodeTarget(Node _target)
    {
        target = _target;
    }

    // Start is called before the first frame update    
    void Start()
    {
        buildManager = BuildManager.instance;
       
        for (int i = 0; i < towersBlueprintTable.Length; i++)
        {
            if (i < 5)
            {
                towersBlueprintTable[i].prefab = buildManager.Towers[i].towersPrefab[i];               
            }               
            else if (i < 9)
            {
                towersBlueprintTable[i].prefab = buildManager.Towers[0].towersPrefab[i-4];
            }
            else if (i < 12)
            {
                towersBlueprintTable[i].prefab = buildManager.Towers[1].towersPrefab[i - 7];
            }
            else if (i < 14)
            {
                towersBlueprintTable[i].prefab = buildManager.Towers[2].towersPrefab[i - 9];
            }
            else if(i < 15)
            {
                towersBlueprintTable[i].prefab = buildManager.Towers[3].towersPrefab[i - 10];
            }
            towersBlueprintTable[i].cost = (int)towersBlueprintTable[i].prefab.GetComponent<Turret>().cost;

        }
        fireTowerCost.text = towersBlueprintTable[0].cost + " souls";
        windTowerCost.text = towersBlueprintTable[1].cost + " souls";
        lightningTowerCost.text = towersBlueprintTable[2].cost + " souls";
        earthTowerCost.text = towersBlueprintTable[3].cost + " souls";
        waterTowerCost.text = towersBlueprintTable[4].cost + " souls";

    }
    
    void isPurchasable(Button button, TurretBlueprint _turretBlueprint)
    {
        if (LevelStatus.Money < _turretBlueprint.cost)
        {
            button.interactable = false;
        }
        else
            button.interactable = true;
    }

    private void Update()
    {
        isPurchasable(fireTowerButton, towersBlueprintTable[0]);
        isPurchasable(windTowerButton, towersBlueprintTable[1]);
        isPurchasable(lightningTowerButton, towersBlueprintTable[2]);
        isPurchasable(earthTowerButton, towersBlueprintTable[3]);
        isPurchasable(waterTowerButton, towersBlueprintTable[4]);
    }

    // Update is called once per frame
}
