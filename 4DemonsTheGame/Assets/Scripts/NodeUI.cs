using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public GameObject shopUi;

    public Shop shop;

    public TextMeshProUGUI upgradeCost;

    public TextMeshProUGUI sell;

    private Node target;

    public Button upgradeButton;

    public bool isUiActive;
    
    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = new Vector2(target.transform.position.x,(target.transform.position.y + 0.10f));

        ui.SetActive(true);
        isUiActive = true;

        if (!target.isUpgraded)
        {
            upgradeCost.text = target.turretBlueprint.cost * 1.2f + "$";
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

    public void OpenShop(Node _target)
    {
        target = _target; //set the node targeted

        shop.SetNodeTarget(target); //give the targeted ndoe to the shop

        shopUi.transform.position = new Vector2(target.transform.position.x, (target.transform.position.y + 1f)); //set the transform of the shop

        shopUi.SetActive(true); //activate the shop     
    }

    public void Hide()
    {
        ui.SetActive(false);
        isUiActive = false;
        shopUi.SetActive(false);
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
