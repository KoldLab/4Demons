using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Enemy attributes :")]
    public float speed = 5.0f;
    public float originalSpeed;
    public int souls = 10;
    public float hp = 100;
    public float damage;
    public float attackRange;
    public float attackSpeed;

    [Space]
    [Header("Enemy Status :")]
    public bool isAfterPlayer;
    public GameObject player;
    public bool canAttack = false;
    public float timeBtwAttack;
    public float distanceToEndPoint;

    [Space]
    [Header("Unity Stuff :")]
    public GameObject enemyParticles;
    public Image hpBar;
    public Animator anim;
    private float startingHp;
    private bool isDead = false;
    private bool isCoroutineExecuting = false;
    public GameObject blood;
    

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = speed;
        timeBtwAttack = 1 / attackSpeed;
        startingHp = hp;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        GameObject _blood = (GameObject)Instantiate(blood, transform.position, transform.rotation);
        Destroy(_blood, 2);
        hpBar.fillAmount = hp/startingHp;

        if (hp <= 0 && !isDead)
        {
            Die();
        }
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
        Debug.Log("Enemy Attack!");
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
