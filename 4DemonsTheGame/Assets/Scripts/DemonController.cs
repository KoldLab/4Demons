using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    public Demon demon;

    [Space]
    [Header("Character statistics:")]
    public Vector2 movementDirection;
    public float movementSpeed;

    [Space]
    [Header("References:")]
    private Rigidbody2D rb;
    private Animator demonAnimator;

    // Start is called before the first frame update
    void Start()
    {
        demonAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {              
            ProcessInputs();
            Move();
     
    }

    void ProcessInputs()
    {        
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize(); 
    }

    void Move()
    {
        if (GetComponent<Demon>().isAttacking)
        {
            rb.velocity = movementDirection * movementSpeed * 0;
        }
        else
        {
            rb.velocity = movementDirection * movementSpeed * demon.speed;
            Animate();
        }
        
    }

    void Animate()
    {
        if(movementDirection.x != 0 || movementDirection.y != 0)
        {
            if(movementDirection.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if(movementDirection.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            demonAnimator.SetBool("isRunning", true);
        }
        else
        {
            demonAnimator.SetBool("isRunning", false);
        }
        
    }
}
