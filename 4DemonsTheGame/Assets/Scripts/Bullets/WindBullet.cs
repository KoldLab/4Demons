using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBullet : AbstractBullet
{
    [Header("Special bullet Attribute")]
    public float pushBack;


    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);
        Coroutine windDamage = StartCoroutine(WindDamage(e, pushBack));

        while (windDamage != null)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
