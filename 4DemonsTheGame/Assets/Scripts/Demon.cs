using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demon : MonoBehaviour
{    
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator attackAnim;
    [Space]
    [Header("Demon status :")]
    public static bool IsDead;
    public int enemyHandled;
    private float timeBtwAttack;
    [Space]
    [Header("Demon attributes :")]
    public float attackSpeed;
    public float attackRange;
    public int damage;
    public float healthPoints;
    public int enemyThatDemonCanHandle = 3;
    public float defense;
    public float hpRegenBy2Seconds;
    [Space]
    [Header("Unity Stuff :")]   
    public Image hpBar;
    private float startingHp;
    private bool isCoroutineExecuting = false;
    public GameObject blood;
    public bool isAttacking = false;



    // Start is called before the first frame update
    void Start()
    {
        enemyHandled = 0;
        IsDead = false;
        startingHp = healthPoints;
        attackAnim.SetFloat("attackSpeed", attackSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        Regenaration();
        
        if (Input.GetKeyDown(KeyCode.Space)){ //attack
            if (timeBtwAttack == 0)
            {
                StartCoroutine(Attack(1 / attackSpeed));
            }
        }
        if(healthPoints <= 0) //if the demon/player is dead --> GameOver
        {
            IsDead = true;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private IEnumerator Attack(float startTimeBtwAttack)
    {
        if (isCoroutineExecuting)
        {
            yield break;
        }
        isCoroutineExecuting = true;
        timeBtwAttack = startTimeBtwAttack;       
        attackAnim.SetTrigger("attack");
        isAttacking = true;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {           
           enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage);                            
        }
        
        yield return new WaitForSeconds(startTimeBtwAttack);
        isAttacking = false;
        isCoroutineExecuting = false;
        timeBtwAttack = 0;
    }

    public void TakeDamage(float damage)
    {
        float realDamage = damage - defense;
        if(realDamage < 0)
        {
            realDamage = 0;
        }
        healthPoints -= realDamage;
        GameObject _blood = (GameObject)Instantiate(blood, transform.position, transform.rotation);
        Destroy(_blood, 2);
        hpBar.fillAmount = healthPoints / startingHp;
    }

    public void Regenaration()
    {
        if(healthPoints < startingHp)
        {
            healthPoints += hpRegenBy2Seconds * Time.deltaTime * 0.5f;
            hpBar.fillAmount = healthPoints / startingHp;
        }
    }
}
