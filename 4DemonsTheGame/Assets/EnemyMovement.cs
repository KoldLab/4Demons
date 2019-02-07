using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject enemyParticles;
    public GameObject endPoint;
    private Transform target; //target = la cible ou aller soit le waypoints

    // Start is called before the first frame update
    void Start()
    {
        target = endPoint.transform; //on donne le target au premier waypoints quon va update
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.position - transform.position; //creer un vecteur de notre position vers la position a aller... transform.position donne notre position
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            Destroy(gameObject);
        }
        //il faut bouger avec cette direction et on le normalize pour avoir une vitesse de 1 qu'on multipliera par notre vitesse. Time.detaTime sert a ne pas dependre des frames
        // Space.world veut dire le vecteur est dans quel espace
    }
}
