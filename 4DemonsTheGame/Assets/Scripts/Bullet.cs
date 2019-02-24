using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    [Header("Attributes")]
    public float explosionRadius = 0f;
    public float rotationSpeed = 1000f;
    public float speed = 25f;
    public float damage = 50f;

    [Header("Unity Setup Fields")]
    public GameObject explosionPrefab;
    private Transform target;
    private Rigidbody2D rb;



    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector2 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);



    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Enemy")
        {
            
           GameObject bulletExplo = (GameObject)Instantiate(explosionPrefab, transform.position, transform.rotation);


            if (explosionRadius > 0f)
            {
                Explode();
            }
            else
            {
                Damage(target,damage);
            }

            Destroy(gameObject);
            Destroy(bulletExplo, 1);
        }

        return;
    }

    void Explode()
    {
        Vector2 pos = new Vector2(
                    (transform.position.x),
                    (transform.position.y)
                    );

        Vector2 dir = new Vector2(0, 0);

        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(pos, explosionRadius, dir);
        foreach (RaycastHit2D hits in raycastHit2Ds)
        {
            if (hits.transform.tag == "Enemy")
            {   
                    Damage(hits.transform, damage);    
            }
        }

    }

    void Damage(Transform enemy, float damage)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
        {
            e.TakeDamage(damage);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }



}
