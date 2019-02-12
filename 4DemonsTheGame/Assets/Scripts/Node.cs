using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;

    public GameObject turret;

    private Color startColor;
    private SpriteRenderer rend;

    [Header("Optional")]
    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {

        if (buildManager.CanBuild)
        {
            return;
        }
        if (turret != null)
        { 
            Debug.Log("Can't build there!"); //TODO DIsplay on screen
            return;
        }

        //build a turret

        buildManager.BuildTurretOn(this);


    }

    void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

    }
        
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {

    }
}