using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;

    BuildManager buildManager;


    public void PushaseTurretOne()
    {
        buildManager.SetTurretToBuild(buildManager.TurretOnePrefab);
    }

    public void PushaseTurretTwo()
    {
        buildManager.SetTurretToBuild(buildManager.TurretTwoPrefab);
    }

    public void PushaseTurretThree()
    {
        buildManager.SetTurretToBuild(buildManager.TurretThreePrefab);
    }
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
