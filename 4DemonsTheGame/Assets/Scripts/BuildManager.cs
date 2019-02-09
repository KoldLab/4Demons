using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    //only one build manage - singleton
    public static BuildManager instance; //stores a reference to itself

    void Awake()
    {
        //each time we start the game there's only one buildmanager and this variable can be accessed by anywhere
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
        }
        instance = this;
    }


    public GameObject TurretOnePrefab;
    public GameObject TurretTwoPrefab;
    public GameObject TurretThreePrefab;

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
    // Update is called once per frame
    void Update()
    {

    }
}