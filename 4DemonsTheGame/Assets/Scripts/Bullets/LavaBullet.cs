using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBullet : AbstractBullet
{
    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        LavaDamage(e, damage);
        Destroy(gameObject);
        yield return null;
    }

    public void LavaDamage(Enemy _enemy, float damage)
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
            StartCoroutine(FireDamage(enemiesToDamage[i].GetComponent<Enemy>(),damage));
        }
    }
}


