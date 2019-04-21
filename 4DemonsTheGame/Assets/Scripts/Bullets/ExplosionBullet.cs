using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : AbstractBullet
{
    [Header("Special bullet Attribute")]
    public float stunnedTime;

    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        ExplosionDamage(e, damage);
        yield return new WaitForSeconds(1f); //wait for the burned time
        Destroy(gameObject);
    }

    public void ExplosionDamage(Enemy _enemy, float damage)
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
            StartCoroutine(LightningDamage(enemiesToDamage[i].GetComponent<Enemy>(), stunnedTime));
        }
    }
}
