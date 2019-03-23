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
    public TurretBlueprint earthCombinedBlueprint;
    public TurretBlueprint lightningCombinedBlueprint;
    public TurretBlueprint waterCombinedBlueprint;

    [HideInInspector]
    public bool isUpgraded = false;
    public bool isCombined = false;
    public float upgradeCost;

    public PopUpBuiltTurretInfo pop;

    private Color startColor;
    private SpriteRenderer rend;
    

    [Header("Optional")]
    BuildManager buildManager;
    GameController gameController;   
    public Shop shop;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.instance;
        buildManager = BuildManager.instance;
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.material.color;
        
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
        int upgradeCost = (int) (turretBlueprint.cost * 1.2f);
        if (!CanUpgrade())
        {
            return;
        }
        if(type == turret.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().bulletType.ToString())
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
        LevelStatus.Money -= upgradeCost;
    }

    public void NormalUpgrade()
    {
        turret.GetComponent<Turret>().damageBoost = 1.1f;
        isUpgraded = true;
    }

    public void SetCombinedPossibilities()
    {
        switch (turret.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().bulletType)
        {
            case Bullet.Type.Fire:
                fireCombinedBlueprint = shop.towersBlueprintTable[0];
                windCombinedBlueprint = shop.towersBlueprintTable[5];
                earthCombinedBlueprint = shop.towersBlueprintTable[6];
                lightningCombinedBlueprint = shop.towersBlueprintTable[7];
                waterCombinedBlueprint = shop.towersBlueprintTable[8];
                break;
            case Bullet.Type.Wind:
                fireCombinedBlueprint = shop.towersBlueprintTable[5];
                windCombinedBlueprint = shop.towersBlueprintTable[1];
                earthCombinedBlueprint = shop.towersBlueprintTable[9];
                lightningCombinedBlueprint = shop.towersBlueprintTable[10];
                waterCombinedBlueprint = shop.towersBlueprintTable[11];
                break;
            case Bullet.Type.Lightning:
                fireCombinedBlueprint = shop.towersBlueprintTable[6];
                windCombinedBlueprint = shop.towersBlueprintTable[9];
                earthCombinedBlueprint = shop.towersBlueprintTable[2];
                lightningCombinedBlueprint = shop.towersBlueprintTable[12];
                waterCombinedBlueprint = shop.towersBlueprintTable[13];
                break;
            case Bullet.Type.Earth:
                fireCombinedBlueprint = shop.towersBlueprintTable[7];
                windCombinedBlueprint = shop.towersBlueprintTable[10];
                earthCombinedBlueprint = shop.towersBlueprintTable[12];
                lightningCombinedBlueprint = shop.towersBlueprintTable[3];
                waterCombinedBlueprint = shop.towersBlueprintTable[14];
                break;
            case Bullet.Type.Water:
                fireCombinedBlueprint = shop.towersBlueprintTable[8];
                windCombinedBlueprint = shop.towersBlueprintTable[11];
                earthCombinedBlueprint = shop.towersBlueprintTable[13];
                lightningCombinedBlueprint = shop.towersBlueprintTable[14];
                waterCombinedBlueprint = shop.towersBlueprintTable[4];

                break;

            default:
                System.Console.WriteLine("Default case");
                break;
        }


    }

    public void Combine(TurretBlueprint combinedTurret)
    {
        if (CanCombine(combinedTurret.cost))
        {
            Destroy(turret);
            buildManager.BuildTurret(combinedTurret, this);
            isCombined = true;
        }
        else
            Debug.Log("Can't combine");
    }
    public bool CanUpgrade()
    {
        if(turretBlueprint != null)
        {
            int upgradeCost = (int)(turretBlueprint.cost * 1.2f);
            if (LevelStatus.Money < upgradeCost)
            {
                return false;
            }
            else
                return true;
        }
        return false;
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

    //Sell Turret
    public void SellTurret()
    {
        if (!this.isUpgraded)
        {
            LevelStatus.Money += turretBlueprint.GetSellAmount();
        }
        else
        {
            LevelStatus.Money += turretBlueprint.GetUpgradedSellAmount();
        }
        //get rid of old turret
        isUpgraded = false;
        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        shop = Resources.FindObjectsOfTypeAll<Shop>()[0];
        Debug.Log(buildManager.GetTurretToBuild() == null);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if(turret != null)
        {
            SetCombinedPossibilities();
        }
            rend.material.color = hoverColor;
    
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

    }
}