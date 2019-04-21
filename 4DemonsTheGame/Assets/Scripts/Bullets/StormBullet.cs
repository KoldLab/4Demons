using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormBullet : AbstractBullet
{
    [Header("Special bullet Attribute")]
    public float slow;
    public float stunned;


    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);

        Coroutine lightningDamage = StartCoroutine(LightningDamage(e, slow));
        while (lightningDamage != null)
        {
            yield return null;
        }
        Coroutine waterDamage = StartCoroutine(WindDamage(e, stunned));
        while (waterDamage != null)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
