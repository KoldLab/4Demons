using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //bloquer la camera
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
            return;

      
        if (transform.position.x <= 6f && (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness))
        {
            transform.Translate(Vector2.right * panSpeed * Time.deltaTime);
        }
        if (transform.position.x >= -6 && (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness))
        {
            transform.Translate(Vector2.left * panSpeed * Time.deltaTime);
        }
        



    }
}