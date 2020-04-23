using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{

    [Header("Enemy attributes :")]
    public int souls = 10;
    public int xp = 5;

    [Space]
    [Header("Enemy Status :")]
    public bool isAfterPlayer;
    public GameObject player;
    public bool canAttack = false;
    public float distanceToEndPoint;

    [Space]
    [Header("Unity Stuff :")]
    public GameObject enemyParticles;
    public Animator anim;

    

    // Start is called before the first frame update
    public override void Start()
    {
        originalSpeed = speed;
        timeBtwAttack = 1 / attackSpeed;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        anim = GetComponent<Animator>();
        base.Start();
    }

    public override bool TakeDamage(float amount)
    {
        if (base.TakeDamage(amount) && !isDead)
        {
            Die();
            return true;
        }
        return false;
    }

    IEnumerator Attack()
    {
        if (isCoroutineExecuting)
        {   
            yield break;
        }
        isCoroutineExecuting = true;
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(1/attackSpeed);
        player.GetComponent<Demon>().TakeDamage(damage);
        timeBtwAttack = 1 / attackSpeed;          
        canAttack = false;
        isCoroutineExecuting = false;
    }


    void Die()
    {
        isDead = true;
        isAfterPlayer = false;
        player.GetComponent<Demon>().enemyHandled--;
        GameObject effect = (GameObject)Instantiate(enemyParticles, new Vector2(transform.position.x, transform.position.y + 0.35f), transform.rotation);
        Destroy(effect, 1.08f);
        WaveSpawner.EnemiesLeft--;
        LevelStatus.EnemyKilled++;
        Player.Instance.enemyKilled++;
        Destroy(gameObject);
        LevelStatus.Money += souls;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAfterPlayer && canAttack)
        {
            if(Vector2.Distance(transform.position, player.transform.position) <= attackRange)
            {
               StartCoroutine(Attack());
            }
        }
        
        if(timeBtwAttack <= 0)
        {
            canAttack = true;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;           
        }

    }
}
