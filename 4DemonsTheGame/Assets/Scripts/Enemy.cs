using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject enemyParticles;
    [HideInInspector]
    

    public GameObject endPoint;

    public int souls = 10;

    private Transform target; //target = la cible ou aller soit le waypoints

    public float hp = 100;
    private float startingHp;

    [Header("Unity Stuff")]
    public Image hpBar;

    // Start is called before the first frame update
    void Start()
    {
        startingHp = hp;
        target = endPoint.transform; //on donne le target au premier waypoints quon va update
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
        LevelStatus.Money += souls;
        GameObject effect = (GameObject)Instantiate(enemyParticles, transform.position, transform.rotation);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.position - transform.position; //creer un vecteur de notre position vers la position a aller... transform.position donne notre position
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            LevelStatus.LifePoint -= 1;
            Destroy(gameObject);
        }
        //il faut bouger avec cette direction et on le normalize pour avoir une vitesse de 1 qu'on multipliera par notre vitesse. Time.detaTime sert a ne pas dependre des frames
        // Space.world veut dire le vecteur est dans quel espace
    }
}
