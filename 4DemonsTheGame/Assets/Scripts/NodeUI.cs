using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public TextMeshProUGUI upgradeCost;

    public TextMeshProUGUI sell;

    private Node target;

    public Button upgradeButton;
    
    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = new Vector2(target.transform.position.x,(target.transform.position.y + 0.10f));

        ui.SetActive(true);

        if (!target.isUpgraded)
        {
            upgradeCost.text = target.turretBlueprint.upgradeCost + "$";
            upgradeButton.interactable = true;
            sell.text = target.turretBlueprint.GetSellAmount() + "$";
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
            sell.text = target.turretBlueprint.GetUpgradedSellAmount() + "$"; 
        }     
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
