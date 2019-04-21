using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFireBullet : AbstractBullet
{
    [Header("Special bullet Attribute")]
    public float stunnedTime;


    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);

        Coroutine fireDamage = StartCoroutine(FireDamage(e, damage));
        Coroutine lightningDamage = StartCoroutine(LightningDamage(e, stunnedTime));
        while (fireDamage != null && lightningDamage != null)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
