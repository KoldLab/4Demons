using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{

    [Header("Attributes")]
    public float explosionRadius = 0f;
    public float rotationSpeed = 1000f;
    public float speed = 25f;
    public float bulletDamage = 50f;
    public Type bulletType;
    public Effect bulletEffect;
    public enum Type {Fire, Lightning, Water, Earth, Wind, Scorch,LightningFire,Lava,Boil,Swift,Sand,Ice,Explosion,Storm,Wood};
    public enum Effect {Burn, Stun, Slow, Splash, Push};


    [Header("Unity Setup Fields")]
    public GameObject explosionPrefab;
    public Turret myTower; 
    private Transform target;


    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 moveTo = new Vector2(target.position.x, target.position.y);
        transform.position = Vector2.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
        
        //Vector3 dir = target.position - transform.position;

        //float distanceThisFrame = speed * Time.deltaTime;

        //float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        //Vector2 direction = target.position - transform.position; //creer un vecteur de notre position vers la position a aller... transform.position donne notre position
        //transform.Translate(direction.normalized * speed * Time.deltaTime);


    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
           GameObject bulletExplo = (GameObject)Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);

          
                Damage(target,bulletDamage);


            gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(bulletExplo, 0.5f);
        }

        return;
    }


    void Damage(Transform enemy, float _damage)
    {

        Enemy e = enemy.GetComponent<Enemy>();

        float damage = _damage * myTower.damageBoost;

        if(e != null)
        {
            e.TakeDamage(damage);
            switch (this.bulletType)
            {
                case Type.Fire:
                    StartCoroutine(FireDamage(enemy.gameObject, damage, Enemy.Status.Burned,false));
                    break;
                case Type.Wind:
                    StartCoroutine(WindDamage(enemy.gameObject, damage, Enemy.Status.PushedBack,false));
                    break;
                case Type.Lightning:
                    StartCoroutine(LightningDamage(enemy.gameObject,damage, Enemy.Status.Stunned,false));
                    break;
                case Type.Earth:
                    EarthDamage(enemy.gameObject, damage,false);
                    break;
                case Type.Water:
                    StartCoroutine(WaterDamage(enemy.gameObject,damage,false));
                    break;
                case Type.Scorch:
                    StartCoroutine(ScorchDamage(enemy.gameObject, damage, Enemy.Status.Scorched));                                    
                    break;
                case Type.LightningFire:
                    StartCoroutine(LightningFireDamage(enemy.gameObject, damage, Enemy.Status.LightningFire));
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

        }

    }
    IEnumerator FireDamage(GameObject enemy, float damage, Enemy.Status status, bool combo)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if(e.enemyStatus != status)
        {
            Debug.Log("Enemy status = burned");
            e.enemyStatus = status;
            for (int i = 0; i < 5; i++)
            {
                
                if (e == null)
                {
                    yield break;
                }
                enemy.GetComponent<Enemy>().TakeDamage(damage / 10f);
                enemy.GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSecondsRealtime(.1f);
                
                
                
            }
            enemy.GetComponent<Renderer>().material.color = Color.white;
        }
        if (!combo)
        {
            Destroy(gameObject);
        }
    }

    public void EarthDamage(GameObject enemy, float damage, bool combo)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        Vector2 pos = new Vector2(
                    (transform.position.x),
                    (transform.position.y)
                    );

        Vector2 dir = new Vector2(0, 0);

        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(pos, explosionRadius, dir);
        int i = 0;
        foreach (RaycastHit2D hits in raycastHit2Ds)
        {
            if (hits.transform.tag == "Enemy")
            {
                if(i == 0)
                {
                    i++;
                }
                else
                {
                    hits.transform.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                }
                
            }
        }
        if (!combo)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaterDamage(GameObject enemy, float damage, bool combo)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        float slow = damage / 10;
        
        if (e.enemyStatus != Enemy.Status.Slowed)
        {
            if (e == null)
            {
                yield break;               
            }
            e.speed /= slow;
            e.enemyStatus = Enemy.Status.Slowed;
            enemy.GetComponent<Renderer>().material.color = Color.blue;
            yield return new WaitForSecondsRealtime(.5f);
            if (e == null)
            {
                yield break;
            }
            e.speed *= slow;
            e.enemyStatus = Enemy.Status.Normal;
            enemy.GetComponent<Renderer>().material.color = Color.white;

        }
        if (!combo)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WindDamage(GameObject enemy, float damage, Enemy.Status status, bool combo)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        
        float pushBack = damage/500f;
        for (int i = 0; i < 5; i++)
        {
            if (e == null)
            {
                yield break;
            }          
            Vector2 moveTo = new Vector2(e.transform.position.x - pushBack, e.transform.position.y);
            e.transform.position = Vector2.MoveTowards(e.transform.position, moveTo, speed * Time.deltaTime);
            
                      
            yield return null;            
        }
        if (!combo)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator LightningDamage(GameObject enemy, float damage,Enemy.Status status, bool combo)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        
        float stunnedTime = damage / 150f;
        float originalSpeed = e.speed;
            if (e == null)
            {
                yield break;
            }
            e.speed = 0;
            e.enemyStatus = status;
            enemy.GetComponent<Renderer>().material.color = Color.yellow;          
            yield return new WaitForSecondsRealtime(stunnedTime);
            if (e == null)
            {
                yield break;
            }
            e.speed = originalSpeed;
            e.enemyStatus = Enemy.Status.Normal;
            enemy.GetComponent<Renderer>().material.color = Color.white;
        

        if (!combo)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ScorchDamage(GameObject enemy, float damage, Enemy.Status status)
    {
        Coroutine fireDamage = StartCoroutine(FireDamage(enemy,damage,status,true));
        Coroutine windDamage = StartCoroutine(WindDamage(enemy,damage,status,true));
        while (fireDamage != null && windDamage != null)
        {
            yield return null;
        }
        enemy.GetComponent<Enemy>().enemyStatus = Enemy.Status.Normal;
        Destroy(gameObject);
    }

    IEnumerator LightningFireDamage(GameObject enemy, float damage, Enemy.Status status)
    {
        Coroutine fireDamage = StartCoroutine(FireDamage(enemy, damage, status, true));
        Coroutine lightningDamage = StartCoroutine(LightningDamage(enemy, damage, status, true));
        while (fireDamage != null && lightningDamage != null)
        {
            yield return null;
        }
        Debug.Log("Enemy status = normal");
        enemy.GetComponent<Enemy>().enemyStatus = Enemy.Status.Normal;
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }



}
