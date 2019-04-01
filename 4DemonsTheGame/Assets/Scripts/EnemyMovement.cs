using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    public GameObject endPoint;

    public Enemy enemy;
    

    
    // Start is called before the first frame update
    void Start()
    {
        enemy.isKnockedBack = false;
        enemy.isAfterPlayer = false;
        enemy = GetComponent<Enemy>();
        target = endPoint.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if(Vector2.Distance(transform.position, enemy.player.transform.position) > enemy.attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.player.transform.position, enemy.speed * Time.deltaTime);
        }
        
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
                                  
            Debug.Log("Player hit");
            enemy.player = collider.gameObject;

            //collider.gameObject.GetComponent<Demon>().takeDamage(enemy.damage);
            //StartCoroutine(KnockBack());
            //Destroy(collider.gameObject);
        }

        return;
    }

    public IEnumerator KnockBack()
    {
        enemy.isKnockedBack = true;
        for (int i = 0; i < 5; i++)
        {
            if (this == null)
            {
                yield break;
            }
            Vector2 moveTo = new Vector2(transform.position.x - 0.5f, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, moveTo, enemy.speed * Time.deltaTime);

            yield return null;
        }

        enemy.isKnockedBack = false;
    }

    void EndPath()
    {
        LevelStatus.LifePoint--;
        WaveSpawner.EnemiesLeft--;
        Destroy(gameObject);
        
    }
}
