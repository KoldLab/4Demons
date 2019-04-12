using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : AbstractBullet
{
    [Header("Special bullet Attribute")]
    public int slow;

    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();

        e.TakeDamage(damage);

        Coroutine waterDamage = StartCoroutine(WaterDamage(e, slow));

        while (waterDamage != null)
        {
            yield return null;
        }
        Destroy(gameObject);

        
    }
}
