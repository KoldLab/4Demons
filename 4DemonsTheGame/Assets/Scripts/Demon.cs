using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demon : MonoBehaviour
{
    public float timeBtwAttack;
    

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator attackAnim;
    [Space]
    [Header("Demon status :")]
    public static bool IsDead;
    public int enemyHandled;
    [Space]
    [Header("Demon attributes :")]
    public float attackSpeed;
    public float attackRange;
    public int damage;
    public float healthPoints;
    public int enemyThatDemonCanHandle = 3;
    [Space]
    [Header("Unity Stuff :")]   
    public Image hpBar;
    private float startingHp;



    // Start is called before the first frame update
    void Start()
    {
        enemyHandled = 0;
        IsDead = false;
        startingHp = healthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        timeBtwAttack = startTimeBtwAttack;
        attackAnim.SetFloat("attackSpeed", attackSpeed);
        attackAnim.SetTrigger("attack");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);

        }
        yield return new WaitForSeconds(startTimeBtwAttack);
        timeBtwAttack = 0;
    }

    public void takeDamage(float damage)
    {
        Debug.Log(damage);
        healthPoints -= damage;
        hpBar.fillAmount = healthPoints / startingHp;
    }
}
