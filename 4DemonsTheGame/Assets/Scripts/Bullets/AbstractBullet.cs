using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBullet : MonoBehaviour
{

    [Space]
    [Header("Attributes")]
    public float explosionRadius;
    public float speed = 10;
    public Type bulletType;
    public Effect bulletEffect;
    public enum Type { Fire, Lightning, Water, Earth, Wind, Scorch, LightningFire, Lava, Boil, Swift, Sand, Ice, Explosion, Storm, Wood };
    public enum Effect { Burn, Stun, Slow, Splash, Push };

    [Space]
    [Header("Unity Setup Fields")]
    public GameObject explosionPrefab;
    public Turret myTower;
    protected Transform target;
    public LayerMask whatIsEnemies;
    public Collider2D b_Collider;

    // Start is called before the first frame update.
    void Start()
    {
        if (target == null) //if the enemy is dead, just destroy the bullet since it has no target
        {
            Destroy(gameObject);
            return;
        }
        whatIsEnemies = LayerMask.GetMask("Enemy"); //Set's the layer mask to Enemy, may be change, if i create a turret that aims for flying enemies
        b_Collider = GetComponent<Collider2D>();
    }
    // Update is called once per frame.
    void Update()
    {
        if (target == null) //if the enemy is dead, just destroy the bullet since it has no target
        {
            Destroy(gameObject);
            return;
        }
        Vector2 moveTo = new Vector2(target.position.x, target.position.y); //create the direction vector when the bullet should go, which changes every frame since the enemy is moving
        transform.position = Vector2.MoveTowards(transform.position, moveTo, speed * Time.deltaTime); //change the position to the point the the direction vector generates each frames
    }
    //Seek is called when the tower shoots the bullet, it goes for the enemy that is the target.
    public void Seek(Transform _target)
    {
        target = _target; //take the target from the turret and give it to the bullet
    }
    //OnTriggerEnter2D is trigger when the bullet hit a collider, but something happen if and only if it's an enemy.
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            GameObject bulletExplo = (GameObject)Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation); // instanciate the animation of the explosion

            Hit(target, myTower.damage); //call the Hit function that is used to inflicts damage to the enemy(it changes depending on the type of bullet)

            gameObject.GetComponent<Renderer>().enabled = false; //we hide the bullet,because if it has a long effect, the object bullet has to stay up the time the effect ends
            Destroy(bulletExplo, 0.5f);//destroy the explosion after the animation is completed
        }

        return;
    }
    //Hit is called when the bullet hit an enemy.
    void Hit(Transform enemy, float _damage)
    {
        float damage = _damage * myTower.damageBoost;       
        StartCoroutine(bulletSpecialDamage(damage));
        b_Collider.enabled = false;
    }
    //this is the damage that will output the different type of bullets
    abstract public IEnumerator bulletSpecialDamage(float damage);
    //this function draws the range of the bullet explosion in the Scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public IEnumerator FireDamage(Enemy _enemy, float _damage)
    {
        for (int i = 0; i < 5; i++)
        {
            if (_enemy == null)
            {
                yield break;
            }
            _enemy.TakeDamage(_damage / 10f);
            yield return new WaitForSecondsRealtime(.1f);
        }
    }

    public IEnumerator WindDamage(Enemy _enemy, float _pushBackPower)
    {
        for (int i = 0; i < 5; i++)
        {
            if (_enemy == null)
            {
                yield break;
            }
            Vector2 moveTo = new Vector2(_enemy.transform.position.x - _pushBackPower, _enemy.transform.position.y);
            _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, moveTo, speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator LightningDamage(Enemy _enemy, float _stunnedTime)
    {

        if (_enemy == null)
        {
            yield break;
        }
        else { }
        _enemy.speed = 0;

        yield return new WaitForSecondsRealtime(_stunnedTime);

        if (_enemy == null)
        {
            yield break;
        }
        else
        _enemy.speed = _enemy.originalSpeed;
    }

    public void EarthDamage(Enemy _enemy, float damage, IEnumerator coroutine = null)
    {
        Vector2 center = new Vector3(
                    (transform.position.x),
                    (transform.position.y)
                    );
        float radius = explosionRadius;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(center, radius, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponentInParent<Enemy>().TakeDamage(damage);
            StartCoroutine(coroutine);
        }
    }

    public IEnumerator WaterDamage(Enemy _enemy, float _slow)
    {
        if (_enemy == null)
        {
            yield break;
        }
        _enemy.speed /= _slow;

        yield return new WaitForSecondsRealtime(.5f);

        if (_enemy == null)
        {
            yield break;
        }
        _enemy.speed *= _slow;
    }

}
