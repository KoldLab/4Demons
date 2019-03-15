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

    public PopUpBuiltTurretInfo pop;

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

    //Build Turret
    public void BuildTurret(TurretBlueprint blueprint)
    {
        LevelStatus.Money -= blueprint.cost; //deduct money 

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, transform.rotation); //instanciate the tower
        turret = _turret; //set the tower on taht node to the built tower
        turretBlueprint = blueprint; //set the blueprint to the blueprint's built tower
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