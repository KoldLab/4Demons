using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : AbstractBullet
{
    [Header("Special bullet Attribute")]
    public float stunnedTime;

    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);

        StartCoroutine(LightningDamage(e,stunnedTime));

        Destroy(gameObject);
        yield return null;
    }
}
