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
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
    public bool isCombined = false;

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
        switch (type)
        {
            case "Fire":
                CombineFire();
                break;
            case "Wind":
                
                break;
            case "Lightning":
                
                break;
            case "Earth":
                
                break;
            case "Water":
                
                break;

            default:
                NormalUpgrade();
                break;
        }
        LevelStatus.Money -= upgradeCost;
    }

    public void NormalUpgrade()
    {
        turret.GetComponent<Turret>().damageBoost = 1.1f;
        isUpgraded = true;
    }
    public void CombineFire()
    {
        switch (turret.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().bulletType)
        {
            case Bullet.Type.Fire:
                turret.GetComponent<Turret>().damageBoost = 1.1f;
                isUpgraded = true;
                break;
            case Bullet.Type.Wind:
                Debug.Log("WindCombine");
                Destroy(turret);
                GameObject _tower1 = (GameObject)Instantiate(buildManager.Towers[0].towersPrefab[1], transform.position, buildManager.Towers[0].towersPrefab[1].transform.rotation);
                turret = _tower1;
                turretBlueprint = shop.towersBlueprintTable[5];
                isCombined = true;
                break;
            case Bullet.Type.Lightning:
                Destroy(turret);
                GameObject _tower2 = (GameObject)Instantiate(buildManager.Towers[0].towersPrefab[2], transform.position, buildManager.Towers[0].towersPrefab[2].transform.rotation);
                turret = _tower2;
                turretBlueprint = shop.towersBlueprintTable[5];
                isCombined = true;
                break;
            case Bullet.Type.Earth:
                break;
            case Bullet.Type.Water:
                break;

            default:
                System.Console.WriteLine("Default case");
                break;
        }
    }
    public void CombineWind()
    {

    }
    public void CombineLightning()
    {

    }
    public void CombineEarth()
    {

    }
    public void CombineWater()
    {

    }
    public bool CanUpgrade()
    {
        int upgradeCost = (int)(turretBlueprint.cost * 1.2f);
        if (LevelStatus.Money < upgradeCost)
        {
            return false;
        }
        else
            return true;
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
        Debug.Log(buildManager.GetTurretToBuild() == null);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
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