using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    //only one build manage - singleton
    public static BuildManager instance; //stores a reference to itself
    private bool active = false;
    public GameController gameController;

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

    public bool CanBuild
    {
        get { return turretToBuild == null;  }
    }
    public bool HasMoney
    {
        get { return LevelStatus.Money >= turretToBuild.cost; }

    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public void BuildTurretOn(Node node)
    {
        if(LevelStatus.Money < turretToBuild.cost)
        {
            return;
        }
        GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.transform.position, node.transform.rotation);
        node.turret = turret;
        LevelStatus.Money -= turretToBuild.cost;
        turretToBuild = null;
        this.setActive(false);
        gameController.Resume();
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
}