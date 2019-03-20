using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Stats")]
    public float speed = 5.0f;
    public int souls = 10;
    public float hp = 100;
    public Status enemyStatus;
    
    [Header("Unity Stuff")]
    public GameObject enemyParticles;
    public Image hpBar;
    private float startingHp;
    private bool isDead = false;
    public enum Status {Normal, Burned, Slowed, Stunned}

    // Start is called before the first frame update
    void Start()
    {
        startingHp = hp;
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;

        hpBar.fillAmount = hp/startingHp;

        if (hp <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GameObject effect = (GameObject)Instantiate(enemyParticles, new Vector2(transform.position.x, transform.position.y + 0.35f), transform.rotation);
        Destroy(effect, 1.08f);
        WaveSpawner.EnemiesLeft--;
        LevelStatus.EnemyKilled++;
        Destroy(gameObject);
        LevelStatus.Money += souls;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
