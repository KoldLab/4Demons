using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    public GameObject endPoint;

    private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();

        target = endPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.position - transform.position; //creer un vecteur de notre position vers la position a aller... transform.position donne notre position
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            EndPath();
        }
        //il faut bouger avec cette direction et on le normalize pour avoir une vitesse de 1 qu'on multipliera par notre vitesse. Time.detaTime sert a ne pas dependre des frames
        // Space.world veut dire le vecteur est dans quel espace
    }

    void EndPath()
    {
        LevelStatus.LifePoint--;
        WaveSpawner.EnemiesLeft--;
        Destroy(gameObject);
        
    }
}
