using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : AbstractBullet
{
    [Header("Special bullet Attribute")]
    public float pushBack;
    public float slow;


    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);

        Coroutine waterDamage = StartCoroutine(WaterDamage(e, slow));
        Coroutine windDamage = StartCoroutine(WindDamage(e, pushBack));
        while (waterDamage != null && windDamage != null)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
