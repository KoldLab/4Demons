using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBullet : AbstractBullet
{
    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);

        float pushBack = damage / 100f;
        Coroutine windDamage = StartCoroutine(WindDamage(e, pushBack));

        while (windDamage != null)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
