using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Color startColor;
    private SpriteRenderer rend;
    

    [Header("Optional")]
    BuildManager buildManager;
    GameController gameController;
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
       
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }

        //build a turret     
        BuildTurret(buildManager.getTurretToBuild());
        
    }

    //Build Turret
    void BuildTurret(TurretBlueprint blueprint)
    {

        if (LevelStatus.Money < blueprint.cost)
        {
            gameController.Resume();
            return;
        }
        LevelStatus.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, transform.rotation);
        turret = _turret;
        turretBlueprint = blueprint;
        buildManager.cancelBuild();
    }

    //Upgrade Turret
    public void UpgradeTurret()
    {
        if (LevelStatus.Money < turretBlueprint.upgradeCost)
        {
            return;
        }
        LevelStatus.Money -= turretBlueprint.upgradeCost;
        
        //get rid of old turret
        Destroy(turret);
        Debug.Log("Turret Destroyed");
        //build upgraded one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, transform.position, transform.rotation);
        turret = _turret;
        
        Debug.Log("Turret Upgraded");
        isUpgraded = true;     

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

        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

    }
        
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {

    }
}