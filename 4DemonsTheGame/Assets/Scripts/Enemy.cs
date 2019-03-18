using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject enemyParticles;
  
    public int souls = 10;

    public float hp = 100;
    private float startingHp;

    private bool isDead = false;

    [Header("Unity Stuff")]
    public Image hpBar;

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
