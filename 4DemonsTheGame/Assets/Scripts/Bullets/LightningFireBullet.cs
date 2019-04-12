using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFireBullet : AbstractBullet
{
    public override IEnumerator bulletSpecialDamage(float damage)
    {
        b_Collider.enabled = false;
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);
        float pushBack = damage / 100f;


        Coroutine fireDamage = StartCoroutine(FireDamage(e, damage));
        Coroutine lightningDamage = StartCoroutine(LightningDamage(e, pushBack));
        while (fireDamage != null && lightningDamage != null)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
