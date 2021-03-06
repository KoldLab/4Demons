﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public GameObject shopUi;

    public Shop shop;

    [Header("UpgradeText")]
    public TextMeshProUGUI upgradeCost;
    public TextMeshProUGUI combineFireCost;
    public TextMeshProUGUI combineWindCost;
    public TextMeshProUGUI combineLightningCost;
    public TextMeshProUGUI combineEarthCost;
    public TextMeshProUGUI combineWaterCost;
    public TextMeshProUGUI sell;
    [Header("Buttons")]
    public Button upgradeButton;
    public Button[] CombineButtons;

    private Node target;


    public bool isUiActive;
    
    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = new Vector2(target.transform.position.x,(target.transform.position.y + 0.50f));

        ui.SetActive(true);
        isUiActive = true;

        if (!target.isCombined)
        {
            combineFireCost.text = target.fireCombinedBlueprint.cost + " souls";
            combineWindCost.text = target.windCombinedBlueprint.cost + " souls";
            combineLightningCost.text = target.lightningCombinedBlueprint.cost + " souls";
            combineEarthCost.text = target.earthCombinedBlueprint.cost + " souls";
            combineWaterCost.text = target.waterCombinedBlueprint.cost + " souls";
        }

        if (!target.isUpgraded)
        {
            upgradeCost.text = target.turretBlueprint.cost * 1.2f + " souls";
            upgradeButton.interactable = true;
            sell.text = target.turretBlueprint.GetSellAmount() + " souls";
          
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
            sell.text = target.turretBlueprint.GetSellAmount() + " souls"; 
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

    public void Upgrade(string type)
    {
        target.UpgradeTurret(type); //this node upgradeturret
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret(); //this node sell turret
        BuildManager.instance.DeselectNode();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (!target.isUpgradable && !target.isUpgraded)
            {
                upgradeCost.color = Color.red;             
            }
            else
            {

                upgradeCost.color = Color.white;              
            }
            
        }
        if(target != null)
        {
            if (target.isCombined)
            {
                foreach (Button button in CombineButtons)
                {                    
                    button.interactable = false;
                }
            }
            else
            {
                foreach (Button button in CombineButtons)
                {
                    button.interactable = true;
                }
            }
        }

        
    }
}
