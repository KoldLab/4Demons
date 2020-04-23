using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Space]
    [Header("Attributes :")]
    //Defense
    public float healthPoints;
    public float defense;
    public float hpRegenBy2Seconds;
    protected float startingHp;
    protected bool isDead;

    //Movemment
    public float speed;
    public float originalSpeed;

    //Attack
    public float damage;
    public float attackSpeed;
    public float attackRange;
    public float timeBtwAttack;

    // Start is called before the first frame update 
    [Space]
    [Header("Status :")]
    public static bool IsDead;

    [Space]
    [Header("Unity Stuff :")]
    public Image hpBar;
    protected bool isCoroutineExecuting = false;
    public GameObject blood;
    public SpriteRenderer body;

    public virtual void Start()
    {
        startingHp = healthPoints;
        IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(float damage)
    {
        float realDamage = damage - defense;
        if (realDamage < 0)
        {
            realDamage = 0;
        }
        healthPoints -= realDamage;
        GameObject _blood = (GameObject)Instantiate(blood, transform.position, transform.rotation);
        Destroy(_blood, 2);
        hpBar.fillAmount = healthPoints / startingHp;
    }
}
