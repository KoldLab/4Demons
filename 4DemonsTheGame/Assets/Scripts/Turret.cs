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
    public bool closestEnemy = false;
    public bool farthestEnemy = false;
    public bool firstEnemy = true;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
      firstEnemy = true;

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

    }

    void ShortestEnemy(GameObject[] enemies)
    {
        float shortestDistance = int.MaxValue;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) //on trouve tous les enemy si on trouve un enemy + proche, on switch target
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
    }

    void FarthestEnemy(GameObject[] enemies)
    {
        float largestDistance = int.MinValue;
        GameObject farthestEnemy = null;
        Debug.Log(largestDistance);

        foreach (GameObject enemy in enemies) //on trouve tous les enemy si on trouve un enemy + proche, on switch target
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy > largestDistance)
            {
                largestDistance = distanceToEnemy;
                farthestEnemy = enemy;
                Debug.Log(largestDistance);
            }
            if (farthestEnemy != null && largestDistance <= range)
            {              
                target = farthestEnemy.transform;
            }
            if (target != null && Vector2.Distance(transform.position, target.position) > range)
            {

            }
            else
            {
                target = null;
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
                Debug.Log("Target is " + orderedEnemiesToDamage.First().Value.name);
                target = orderedEnemiesToDamage.Last().Value.transform;
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