using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    [Header("Towers")]
    public GameObject TurretOnePrefab;
    public GameObject TurretTwoPrefab;
    public GameObject ElectricTurret;
    [Header("UI")]
    public NodeUI nodeUI;
    //only one build manage - singleton
    public static BuildManager instance; //stores a reference to itself
    private bool active = false;
    GameController gameController;
    private Node selectedNode;
    

    void Awake()
    {
        //each time we start the game there's only one buildmanager and this variable can be accessed by anywhere
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
        }
        instance = this;
    }

    public bool getActive()
    {
        return this.active;
    } 

    public void setActive(bool state)
    {
        this.active = state;
    }




    private TurretBlueprint turretToBuild;

    public bool CanBuild => !(turretToBuild == null);

    public bool HasMoney(TurretBlueprint _turretBlueprint)
    {
        return LevelStatus.Money >= _turretBlueprint.cost;

    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
    

    public void SelectNode(Node node, bool isOccupied)
    {
        if(selectedNode == node) //if you click ont he same node, it deselects it
        {
            DeselectNode();
            return;
        }
        else
        {
            selectedNode = node;//else you select that node
            if (isOccupied)
            {                
                nodeUI.SetTarget(node); // you open the upgrade tab
            }
            else
            {
                nodeUI.OpenShop(node); //you open the shop menu
            }
            
        }        
    }
    public void BuildTurret(TurretBlueprint turret, Node node) //called from shop
    {
        turretToBuild = turret; //set the turret to see if you have money or more functionnality
        node.BuildTurret(turretToBuild); // build the turret
        DeselectNode(); //deselect the node
    }
    public void DeselectNode()
    {
        selectedNode = null;
        turretToBuild = null;
        nodeUI.Hide();
    }

    public TurretBlueprint getTurretToBuild()
    {
        return turretToBuild;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Start()
    {
        gameController = GameController.instance;
    }
}