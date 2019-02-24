using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

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

    public GameObject TurretOnePrefab;

    public GameObject TurretTwoPrefab;


    private TurretBlueprint turretToBuild;

    public bool CanBuild => !(turretToBuild == null);

    public bool HasMoney
    {
        get { return LevelStatus.Money >= turretToBuild.cost; }

    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
    public NodeUI nodeUI;

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        gameController.Pause();
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint getTurretToBuild()
    {
        return turretToBuild;
    }
 
    public void cancelBuild()
    {
        turretToBuild = null;
        Debug.Log("GameResumed");
        gameController.Resume();
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