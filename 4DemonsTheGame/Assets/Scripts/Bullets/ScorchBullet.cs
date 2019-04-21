using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorchBullet : AbstractBullet
{
    [Header("Special bullet Attribute")]
    public float pushBack;


    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);

        Coroutine fireDamage = StartCoroutine(FireDamage(e,damage));
        Coroutine windDamage = StartCoroutine(WindDamage(e,pushBack));
        while (fireDamage != null && windDamage != null)
        {
            yield return null;
        }

        Destroy(gameObject);
    }


}
