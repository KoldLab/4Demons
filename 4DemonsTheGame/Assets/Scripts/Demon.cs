using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Demon : Character
{    
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator attackAnim;
    public LevelSystem levelSystem = new LevelSystem();

    [Space]
    [Header("Demon status :")]
    public int enemyHandled;
    public int enemyThatDemonCanHandle = 3;

    [Space]
    [Header("Demon level :")]
    public int level;

    [Space]
    [Header("Unity Stuff :")]
    public Image xPBar;
    public bool isAttacking = false;




    // Start is called before the first frame update
    public override void Start()
    {
        enemyHandled = 0;
        attackAnim.SetFloat("attackSpeed", attackSpeed);
        xPBar.fillAmount = levelSystem.GetExperienceNumber() / levelSystem.GetExperienceToNextLevel();
        base.Start();
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
            if (enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage))
            {
                levelSystem.AddExperience(enemiesToDamage[i].GetComponentInParent<Enemy>().xp);
                xPBar.fillAmount = (float)levelSystem.GetExperienceNumber() / (float)levelSystem.GetExperienceToNextLevel();
            }
        }
        
        yield return new WaitForSeconds(startTimeBtwAttack);
        isAttacking = false;
        isCoroutineExecuting = false;
        timeBtwAttack = 0;
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
