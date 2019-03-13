using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTower;
    public TurretBlueprint missileTower;

    public TextMeshProUGUI standardTowerCost;
    public TextMeshProUGUI missileTowerCost;

    public Button standardTowerButton;
    public Button missileTowerButton;

    private Node target;

    BuildManager buildManager;

    public void SelectTurretOne()
    {
        buildManager.BuildTurret(standardTower,target);//when u click on the turret it buys it
                
    }

    public void SelectTurretTwo()
    {
        buildManager.BuildTurret(missileTower, target);//when u click on the turret it buys it
    }

    public void SetNodeTarget(Node _target)
    {
        target = _target;
    }

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        standardTowerCost.text = standardTower.cost + "$";
        missileTowerCost.text = missileTower.cost + "$";
    }

    void isPurchasable(Button button, TurretBlueprint _turretBlueprint)
    {
        if (!buildManager.HasMoney(_turretBlueprint))
        {
            button.interactable = false;
        }
        else
            button.interactable = true;
    }

    private void Update()
    {
        isPurchasable(standardTowerButton, standardTower);
        isPurchasable(missileTowerButton, missileTower);
    }

    // Update is called once per frame
}
