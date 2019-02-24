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

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = (GameObject)Instantiate(enemyParticles, transform.position, transform.rotation);
        Destroy(effect, 5f);
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
        LevelStatus.EnemyKilled++;
        LevelStatus.Money += souls;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
