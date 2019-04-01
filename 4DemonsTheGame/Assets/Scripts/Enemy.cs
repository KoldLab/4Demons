using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Enemy attributes :")]
    public float speed = 5.0f;
    public int souls = 10;
    public float hp = 100;
    public float damage;
    public float attackRange;

    [Space]
    [Header("Enemy Status :")]
    public Status enemyStatus;
    public bool isKnockedBack;
    public bool isAfterPlayer;
    public GameObject player;

    [Space]
    [Header("Unity Stuff :")]
    public GameObject enemyParticles;
    public Image hpBar;
    private float startingHp;
    private bool isDead = false;
    public enum Status {Normal, Burned, PushedBack, Slowed, Stunned, Scorched, LightningFire, Lava, Boil, Swift, Sand, Ice, Explosion, Storm, Wood }

    // Start is called before the first frame update
    void Start()
    {
        startingHp = hp;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
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
        isAfterPlayer = false;
        player.GetComponent<Demon>().enemyHandled--;
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
