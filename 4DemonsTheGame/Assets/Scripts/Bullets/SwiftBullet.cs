using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftBullet : AbstractBullet
{

    [Header("Special bullet Attribute")]
    public float stunnedTime;
    public float pushBack;

    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);

        Coroutine lightningDamage = StartCoroutine(LightningDamage(e, stunnedTime));
        Coroutine windDamage = StartCoroutine(WindDamage(e, pushBack));
        while (lightningDamage != null && windDamage != null)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
