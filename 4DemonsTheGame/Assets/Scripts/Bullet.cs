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
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;

        float distanceThisFrame = speed * Time.deltaTime;

        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90f);

        //Vector2 direction = (Vector2)target.position - rb.position;// give the position that the bullet must go

        //direction.Normalize();//normalize the vector

        //float rotateAmount = Vector3.Cross(direction, transform.up).z; // use cross product to rotate towards direction

        //rb.angularVelocity = -rotateAmount;

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;

        float distanceThisFrame = speed * Time.deltaTime;
    
        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ +90f);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);


    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Enemy")
        {
           GameObject bulletExplo = (GameObject)Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);


            if (explosionRadius > 0f)
            {
                Explode();
            }
            else
            {
                Damage(target,damage);
            }

            Destroy(gameObject);
            Destroy(bulletExplo, 0.5f);
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
