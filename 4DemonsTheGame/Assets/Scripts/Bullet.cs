using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Attributes")]
    public float explosionRadius = 0f;
    public float rotationSpeed = 1000f;
    public float speed = 25f;
    public Type bulletType;
    public Effect bulletEffect;
    public enum Type {Fire, Lightning, Water, Earth, Wind, Scorch,LightningFire,Lava,Boil,Swift,Sand,Ice,Explosion,Storm,Wood};
    public enum Effect {Burn, Stun, Slow, Splash, Push};


    [Header("Unity Setup Fields")]
    public GameObject explosionPrefab;
    public Turret myTower; 
    private Transform target;
    public LayerMask whatIsEnemies;


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
        whatIsEnemies = LayerMask.GetMask("Enemy");

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

          
                Damage(target,myTower.damage);


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
            switch (this.bulletType)
            {
                case Type.Fire:
                    e.TakeDamage(damage);
                    StartCoroutine(FireDamage(enemy.gameObject, damage, Enemy.Status.Burned,false));
                    break;
                case Type.Wind:
                    e.TakeDamage(damage);
                    StartCoroutine(WindDamage(enemy.gameObject, damage, Enemy.Status.PushedBack,false));
                    break;
                case Type.Lightning:
                    e.TakeDamage(damage);
                    StartCoroutine(LightningDamage(enemy.gameObject,damage, Enemy.Status.Stunned,false));
                    break;
                case Type.Earth:
                    EarthDamage(enemy.gameObject, damage,false);
                    break;
                case Type.Water:
                    e.TakeDamage(damage);
                    StartCoroutine(WaterDamage(enemy.gameObject,damage,Enemy.Status.Slowed,false));
                    break;
                case Type.Scorch:
                    e.TakeDamage(damage);
                    StartCoroutine(ScorchDamage(enemy.gameObject, damage, Enemy.Status.Scorched));                                    
                    break;
                case Type.LightningFire:
                    e.TakeDamage(damage);
                    StartCoroutine(LightningFireDamage(enemy.gameObject, damage, Enemy.Status.LightningFire));
                    break;
                case Type.Lava:
                    LavaDamage(enemy.gameObject, damage, Enemy.Status.Lava);
                    break;
                case Type.Boil:
                    e.TakeDamage(damage);
                    StartCoroutine(BoilDamage(enemy.gameObject, damage, Enemy.Status.Boil,true));
                    break;
                case Type.Swift:
                    e.TakeDamage(damage);
                    StartCoroutine(SwiftDamage(enemy.gameObject, damage, Enemy.Status.Swift));
                    break;
                case Type.Sand: 
                    SandDamage(enemy.gameObject, damage, Enemy.Status.Sand);
                    break;
                case Type.Ice:
                    e.TakeDamage(damage);
                    StartCoroutine(IceDamage(enemy.gameObject, damage, Enemy.Status.Ice));
                    break;
                case Type.Explosion:                   
                    ExplosionDamage(enemy.gameObject, damage, Enemy.Status.Explosion);
                    break;
                case Type.Wood:
                    WoodDamage(enemy.gameObject, damage, Enemy.Status.Wood);
                    break;
                case Type.Storm:
                    e.TakeDamage(damage);
                    StartCoroutine(StormDamage(enemy.gameObject, damage, Enemy.Status.Ice));
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
            e.enemyStatus = status;
            for (int i = 0; i < 5; i++)
            {
                
                if (e == null)
                {
                    yield break;
                }
                enemy.GetComponent<Enemy>().TakeDamage(damage / 10f);                
                yield return new WaitForSecondsRealtime(.1f);
                
                
                
            }
            
        }
        if (!combo)
        {
            e.enemyStatus = Enemy.Status.Normal;
            Destroy(gameObject);
        }
    }

    public void EarthDamage(GameObject enemy, float damage, bool combo)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        Vector2 center = new Vector3(
                    (transform.position.x),
                    (transform.position.y)
                    );
        float radius = explosionRadius;



        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(center, radius, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage);
        }
        
        if (!combo)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaterDamage(GameObject enemy, float damage, Enemy.Status status, bool combo)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        float slow = damage / 10;
        
        if (e.enemyStatus != status)
        {
            if (e == null)
            {
                yield break;               
            }
            e.speed /= slow;
            e.enemyStatus = status;
            
            yield return new WaitForSecondsRealtime(.5f);
            if (e == null)
            {
                yield break;
            }
            e.speed *= slow;
            

        }
        if (!combo)
        {
            e.enemyStatus = Enemy.Status.Normal;
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
                    
            yield return new WaitForSecondsRealtime(stunnedTime);
            if (e == null)
            {
                yield break;
            }
            e.speed = originalSpeed;
            e.enemyStatus = Enemy.Status.Normal;
            
        

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
        enemy.GetComponent<Enemy>().enemyStatus = Enemy.Status.Normal;
        Destroy(gameObject);
    }

    public void LavaDamage(GameObject enemy, float damage, Enemy.Status status)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        Vector2 center = new Vector3(
                    (transform.position.x),
                    (transform.position.y)
                    );
        float radius = explosionRadius;


        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(center, radius, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage);
            StartCoroutine(FireDamage(enemiesToDamage[i].transform.gameObject, damage, status, false));
        }
        
        
               
    }

    IEnumerator BoilDamage(GameObject enemy, float damage, Enemy.Status status, bool combo)
    {
        Coroutine fireDamage = StartCoroutine(FireDamage(enemy, damage, status, combo));
        Coroutine waterDamage = StartCoroutine(WaterDamage(enemy, damage, status, combo));
        while (fireDamage != null && waterDamage != null)
        {
            yield return null;
        }
        enemy.GetComponent<Enemy>().enemyStatus = Enemy.Status.Normal;
        Destroy(gameObject);
    }

    IEnumerator SwiftDamage(GameObject enemy, float damage, Enemy.Status status)
    {
        Coroutine windDamage = StartCoroutine(WindDamage(enemy, damage, status, true));
        Coroutine lightningDamage = StartCoroutine(LightningDamage(enemy, damage, status, true));
        while (windDamage != null && lightningDamage != null)
        {
            yield return null;
        }
        enemy.GetComponent<Enemy>().enemyStatus = Enemy.Status.Normal;
        Destroy(gameObject);
    }

    public void SandDamage(GameObject enemy, float damage, Enemy.Status status)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        Vector2 center = new Vector3(
                    (transform.position.x),
                    (transform.position.y)
                    );
        float radius = explosionRadius;


        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(center, radius, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage);
            StartCoroutine(WindDamage(enemiesToDamage[i].transform.gameObject, damage, status, false));
        }


    }

    IEnumerator IceDamage(GameObject enemy, float damage, Enemy.Status status)
    {
        Coroutine windDamage = StartCoroutine(WindDamage(enemy, damage, status, true));
        Coroutine waterDamage = StartCoroutine(WaterDamage(enemy, damage, status, true));
        while (windDamage != null && waterDamage != null)
        {
            yield return null;
        }
        enemy.GetComponent<Enemy>().enemyStatus = Enemy.Status.Normal;
        Destroy(gameObject);
    }

    public void ExplosionDamage(GameObject enemy, float damage, Enemy.Status status)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        Vector2 center = new Vector3(
                    (transform.position.x),
                    (transform.position.y)
                    );
        float radius = explosionRadius;


        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(center, radius, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage);
            StartCoroutine(LightningDamage(enemiesToDamage[i].transform.gameObject, damage, status, false));
        }


    }

    IEnumerator StormDamage(GameObject enemy, float damage, Enemy.Status status)
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
        
        yield return new WaitForSecondsRealtime(stunnedTime);
        for (int i = 0; i < 5; i++)
        {
            e.speed += originalSpeed / 5;
            yield return new WaitForSecondsRealtime(.1f);
        }

        if (e == null)
        {
            yield break;
        }
        e.speed = originalSpeed;
        e.enemyStatus = Enemy.Status.Normal;       
        Destroy(gameObject);
    }

    public void WoodDamage(GameObject enemy, float damage, Enemy.Status status)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        Vector2 center = new Vector3(
                    (transform.position.x),
                    (transform.position.y)
                    );
        float radius = explosionRadius;


        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(center, radius, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage);
            StartCoroutine(WaterDamage(enemiesToDamage[i].transform.gameObject, damage, status, false));
        }


    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }



}
