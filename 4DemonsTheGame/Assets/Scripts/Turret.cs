using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;
   

    [Header("Attributes")]


    public float range = 5f;
    public float fireRate;
    public float damageBoost = 1f;
    public float damage;
    public float cost;
    public float startTimeBtwShots;
    public bool closestEnemy;
    public bool farthestEnemy;
    public bool firstEnemy;
    public bool lastEnemy;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        if (closestEnemy == true)
        {
            ShortestEnemy(enemies);
        }
        else if (farthestEnemy == true)
        {
            FarthestEnemy(enemies);
        }
        else if (firstEnemy == true)
        {
            FirstEnemy(enemies);
        }
        else if(lastEnemy == true)
        {
            LastEnemy(enemies);
        }

    }

    void ShortestEnemy(GameObject[] enemies)
    {
        if (enemies.Length > 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
            Dictionary<float, Collider2D> orderedEnemiesToDamage = enemiesToDamage.ToDictionary(e => Vector2.Distance(transform.position, e.GetComponent<Enemy>().transform.position));
            var list = orderedEnemiesToDamage.Keys.ToList();
            list.Sort();
            Debug.Log("Start");
            foreach (var key in list)
            {
                Debug.Log(key + " : " + orderedEnemiesToDamage[key]);
            }
            Debug.Log("End");
            if (enemiesToDamage.Length > 0)
            {
                Debug.Log("Target is " + orderedEnemiesToDamage[list.First()].name);
                target = orderedEnemiesToDamage[list.First()].transform;
            }
        }
    }

    void FarthestEnemy(GameObject[] enemies)
    {
        if (enemies.Length > 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
            Dictionary<float, Collider2D> orderedEnemiesToDamage = enemiesToDamage.ToDictionary(e => Vector2.Distance(transform.position,e.GetComponent<Enemy>().transform.position));
            var list = orderedEnemiesToDamage.Keys.ToList();
            list.Sort();
            Debug.Log("Start");
            foreach (var key in list)
            {
                Debug.Log(key + " : " + orderedEnemiesToDamage[key]);
            }
            Debug.Log("End");
            if (enemiesToDamage.Length > 0)
            {
                Debug.Log("Target is " + orderedEnemiesToDamage[list.Last()].name);
                target = orderedEnemiesToDamage[list.Last()].transform;
            }
        }
    }


    void FirstEnemy(GameObject[] enemies)
    {
        if(enemies.Length > 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
            Dictionary<float, Collider2D> orderedEnemiesToDamage = enemiesToDamage.ToDictionary(e => e.GetComponent<Enemy>().distanceToEndPoint);
            var list = orderedEnemiesToDamage.Keys.ToList();
            list.Sort();
            Debug.Log("Start");
            foreach (var key in list)
            {
               Debug.Log(key + " : " + orderedEnemiesToDamage[key]);
            }
            Debug.Log("End");
            if (enemiesToDamage.Length > 0)
            {
                Debug.Log("Target is " + orderedEnemiesToDamage[list.First()].name);
                target = orderedEnemiesToDamage[list.First()].transform;
            }
        }
              
    }
    void LastEnemy(GameObject[] enemies)
    {
        if (enemies.Length > 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
            Dictionary<float, Collider2D> orderedEnemiesToDamage = enemiesToDamage.ToDictionary(e => e.GetComponent<Enemy>().distanceToEndPoint);
            var list = orderedEnemiesToDamage.Keys.ToList();
            list.Sort();
            Debug.Log("Start");
            foreach (var key in list)
            {
                Debug.Log(key + " : " + orderedEnemiesToDamage[key]);
            }
            Debug.Log("End");
            if (enemiesToDamage.Length > 0)
            {
                Debug.Log("Target is " + orderedEnemiesToDamage[list.Last()].name);
                target = orderedEnemiesToDamage[list.Last()].transform;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        //firing
        if (fireRate <= 0f)
        {
            if (target == null)
            {
                return;
            }
            Shoot();
            fireRate = 1 / startTimeBtwShots;

        }
        else
        {
            fireRate -= Time.deltaTime;
        }
        

    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, bulletPrefab.transform.rotation);

        AbstractBullet bullet = bulletGO.GetComponent<AbstractBullet>();

        bullet.myTower = this;

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}