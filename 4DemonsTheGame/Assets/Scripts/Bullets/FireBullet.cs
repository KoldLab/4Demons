using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : AbstractBullet
{    
    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);

        Coroutine fireDamage = StartCoroutine(FireDamage(e, damage));

        while(fireDamage != null)
        {
            yield return null;
        }
        Destroy(gameObject);
        
    }
}
