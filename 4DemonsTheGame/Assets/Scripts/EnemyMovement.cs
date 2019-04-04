using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    public GameObject endPoint;
    public Rigidbody2D rb;
    public Enemy enemy;
    public float direction;
    

    
    // Start is called before the first frame update
    void Start()
    {
        enemy.isKnockedBack = false;
        enemy.isAfterPlayer = false;
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        target = endPoint.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.anim.SetFloat("movementSpeed", enemy.speed);
        if((enemy.player.GetComponent<Demon>().enemyHandled) < (enemy.player.GetComponent<Demon>().enemyThatDemonCanHandle)){
            if (!enemy.isAfterPlayer)
            {
                enemy.player.GetComponent<Demon>().enemyHandled++;
                enemy.isAfterPlayer = true;
            }            
        }

        if (enemy.isAfterPlayer)
        {
            followPlayer();
        }
        else
        {
            enemy.anim.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemy.speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            EndPath();
        }       
        
        //il faut bouger avec cette direction et on le normalize pour avoir une vitesse de 1 qu'on multipliera par notre vitesse. Time.detaTime sert a ne pas dependre des frames
        // Space.world veut dire le vecteur est dans quel espace
    }


    void followPlayer()
    {
        if(Vector2.Distance(transform.position, enemy.player.transform.position) > enemy.attackRange) //if not in attack range, runs towards player
        {
            enemy.anim.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, enemy.player.transform.position, enemy.speed * Time.deltaTime);
            if(transform.position.x > enemy.player.transform.position.x) //check if enemy is on the right of the player or left
            {
                transform.eulerAngles = new Vector3(0, 180, 0); 
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
            enemy.anim.SetBool("isRunning", false);
        
        
    }
       
    void EndPath()
    {
        LevelStatus.LifePoint--;
        WaveSpawner.EnemiesLeft--;
        Destroy(gameObject);
        
    }
}
