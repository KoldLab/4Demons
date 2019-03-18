using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;

    [Header("Attributes")]


    public float range = 5f;
    public float fireRate;
    public float startTimeBtwShots;
    public bool closestEnemy = true;
    public bool farthestEnemy = false;
    public bool firstEnemy = false;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
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

        foreach (GameObject enemy in enemies) //on trouve tous les enemy si on trouve un enemy + proche, on switch target
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= range)
            {
                target = enemy.transform;
                break;
            }
            else
            {
                target = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        //firing
        if (fireRate <= 0f)
        {
            if (target == null)
            {
                return;
            }
            Shoot();
            fireRate = startTimeBtwShots;

        }
        else
        {
            fireRate -= Time.deltaTime;
        }
        

    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, bulletPrefab.transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

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