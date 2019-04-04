using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;
    private bool playerNotFound = true;
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float droite = 4.6f;
    public float gauche = 4.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void switchCamMovement()
    {
        if (doMovement == true)
        {
            doMovement = false;
        }
        else
            doMovement = true;
    }
    // Update is called once per frame
    void Update()
    {
       
        //bloquer la camera
        if (GameController.GameIsOver)
        {
            this.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
            return;

      
        if (transform.position.x <= droite && (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness))
        {
            transform.Translate(Vector2.right * panSpeed * Time.deltaTime);
        }
        if (transform.position.x >= -gauche && (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness))
        {
            transform.Translate(Vector2.left * panSpeed * Time.deltaTime);
        }
        



    }

    public void FollowPlayer()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindWithTag("Player").transform;
    }
}