using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private GameObject turret;

    private Color startColor;
    private SpriteRenderer rend;


    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
    }

    void OnMouseDown()
    {

        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }
        if (turret != null)
        {
            Debug.Log("Can't build there!"); //TODO DIsplay on screen
            return;
        }

        //build a turret

        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
        buildManager.SetTurretToNull();

    }

    void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }
        rend.color = hoverColor;
    }
    void OnMouseExit()
    {
        rend.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {

    }
}