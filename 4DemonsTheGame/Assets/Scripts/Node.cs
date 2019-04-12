using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;

   // [HideInInspector]
    public GameObject turret;
    
    public TurretBlueprint turretBlueprint;
    public TurretBlueprint fireCombinedBlueprint;
    public TurretBlueprint windCombinedBlueprint;
    public TurretBlueprint lightningCombinedBlueprint;
    public TurretBlueprint earthCombinedBlueprint;
    public TurretBlueprint waterCombinedBlueprint;

    
    public bool isUpgraded = false;
    public bool isUpgradable = true;
    public bool isCombined = false;
    public float upgradeCost = 1.2f; //change that soon

    public PopUpBuiltTurretInfo pop;

    private Color startColor;
    private SpriteRenderer rend;
    

    [Header("Optional")]
    BuildManager buildManager;
    GameController gameController;
    public NodesParent nodesParent;
    public Shop shop;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.instance;
        buildManager = BuildManager.instance;
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.material.color;
        nodesParent = GameObject.Find("Nodes").GetComponent<NodesParent>();
        
    }

    void OnMouseDown()
    {
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
       
        if (turret != null) //if there's a turret on that node
        {
            buildManager.SelectNode(this,true);
            pop.Hide();
            return;
        }
        else //if there's no turret, means that you want to buy one
        {
            buildManager.SelectNode(this, false);
            return;
        }
        
        
    }

    //Upgrade Turret
    public void UpgradeTurret(string type)
    {        
        if(type == turret.GetComponent<Turret>().bulletPrefab.GetComponent<AbstractBullet>().bulletType.ToString())
        {
            NormalUpgrade();
        }
        else
        {
            switch (type)
            {
                case "Fire":
                    Combine(fireCombinedBlueprint);
                    break;
                case "Wind":
                    Combine(windCombinedBlueprint);
                    break;
                case "Lightning":
                    Combine(lightningCombinedBlueprint);
                    break;
                case "Earth":
                    Combine(earthCombinedBlueprint);
                    break;
                case "Water":
                    Combine(waterCombinedBlueprint);
                    break;

                default:
                    NormalUpgrade();
                    break;
            }
        }       
        
    }

    public void NormalUpgrade()
    {
        
        if (CanUpgrade(turretBlueprint.cost * upgradeCost))
        {
            turret.GetComponent<Turret>().damageBoost = 1.1f;
            isUpgraded = true;
            LevelStatus.Money -= (int) (turretBlueprint.cost * upgradeCost);
        }
        else
            return;
        
    }

    public bool CanUpgrade(float upgradedTowerCost)
    {
        if(turretBlueprint != null)
        {            

            if (LevelStatus.Money < upgradedTowerCost)
            {

                return false;
            }
            else
                return true;
        }
        return false;
    }

    public void Combine(TurretBlueprint combinedTurret)
    {
        if (CanCombine(combinedTurret.cost))
        {
            Destroy(turret);
            buildManager.BuildTurret(combinedTurret, this);
            isCombined = true;
            if (isUpgraded)
            {
                isUpgraded = false;
            }
        }
        else
            return;
    }

    public bool CanCombine(float combineCost)
    {
        if (turretBlueprint != null)
        {
            if (LevelStatus.Money < combineCost)
            {
                return false;
            }
            else
               return true;
        }
        return false;
    }

    public void SetCombinedPossibilities()
    {
        switch (turret.GetComponent<Turret>().bulletPrefab.GetComponent<AbstractBullet>().bulletType)
        {
            case AbstractBullet.Type.Fire:
                Debug.Log("fire =" + shop.towersBlueprintTable[0].cost);
                
                fireCombinedBlueprint = shop.towersBlueprintTable[0];
                windCombinedBlueprint = shop.towersBlueprintTable[5];
                lightningCombinedBlueprint = shop.towersBlueprintTable[6];
                earthCombinedBlueprint = shop.towersBlueprintTable[7];
                waterCombinedBlueprint = shop.towersBlueprintTable[8];
                break;
            case AbstractBullet.Type.Wind:
                fireCombinedBlueprint = shop.towersBlueprintTable[5];
                windCombinedBlueprint = shop.towersBlueprintTable[1];
                lightningCombinedBlueprint = shop.towersBlueprintTable[9];
                earthCombinedBlueprint = shop.towersBlueprintTable[10];
                waterCombinedBlueprint = shop.towersBlueprintTable[11];
                break;
            case AbstractBullet.Type.Lightning:
                fireCombinedBlueprint = shop.towersBlueprintTable[6];
                windCombinedBlueprint = shop.towersBlueprintTable[9];
                lightningCombinedBlueprint = shop.towersBlueprintTable[2];
                earthCombinedBlueprint = shop.towersBlueprintTable[12];
                waterCombinedBlueprint = shop.towersBlueprintTable[13];
                break;
            case AbstractBullet.Type.Earth:
                fireCombinedBlueprint = shop.towersBlueprintTable[7];
                windCombinedBlueprint = shop.towersBlueprintTable[10];
                lightningCombinedBlueprint = shop.towersBlueprintTable[12];
                earthCombinedBlueprint = shop.towersBlueprintTable[3];
                waterCombinedBlueprint = shop.towersBlueprintTable[14];
                break;
            case AbstractBullet.Type.Water:
                fireCombinedBlueprint = shop.towersBlueprintTable[8];
                windCombinedBlueprint = shop.towersBlueprintTable[11];
                lightningCombinedBlueprint = shop.towersBlueprintTable[13];
                earthCombinedBlueprint = shop.towersBlueprintTable[14];
                waterCombinedBlueprint = shop.towersBlueprintTable[4];

                break;

            default:
                System.Console.WriteLine("Default case");
                break;
        }


    }
    //Sell Turret
    public void SellTurret()
    {

            LevelStatus.Money += turretBlueprint.GetSellAmount();
        
        //get rid of old turret
        isUpgraded = false;
        isUpgradable = true;
        isCombined = false;
        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
        shop = nodesParent.shop;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if(turret != null)
        {
            SetCombinedPossibilities();
        }                
        if(turret != null && !buildManager.nodeUI.isUiActive)
        {
            pop.SetTarget(this);
        }
       
    }
        
    void OnMouseExit()
    {
        pop.Hide();
        rend.material.color = startColor;
    }
    // Update is called once per frame
    void Update()
    {
        if(turretBlueprint != null)
        {
            if (CanUpgrade(turretBlueprint.cost * upgradeCost))
            {
                isUpgradable = true;
            }
            else
                isUpgradable = false;
        }
        
    }
}