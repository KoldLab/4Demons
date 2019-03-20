using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    [Header("Towers")]
    public GameObjectsTable[] Towers;
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
    public void BuildTurret(TurretBlueprint blueprint, Node node) //called from shop
    {
        LevelStatus.Money -= blueprint.cost;//deduct money         
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, node.transform.position, blueprint.prefab.transform.rotation); //instanciate the tower
        //_turret.transform.localEulerAngles = new Vector3(0, 0, 0);       
        node.turret =  _turret; // build the turret
        node.turretBlueprint = blueprint;
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