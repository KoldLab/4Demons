using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAttack : MonoBehaviour
{
    private float timeBtwAttack;
    

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator attackAnim;
    [Space]
    [Header("Demon attributes :")]
    public float attackSpeed;
    public float attackRange;
    public int damage;


    // Start is called before the first frame update
    void Start()
    {
        attackAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            //then you can attack
            if (Input.GetKey(KeyCode.Space))
            {
                attackAnim.SetTrigger("fireBreath");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange,whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    
                    
                }
                timeBtwAttack = 1/attackSpeed;
            }
        
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
