using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBullet : AbstractBullet
{

    public override IEnumerator bulletSpecialDamage(float damage)
    {

        Enemy e = target.GetComponent<Enemy>();
        EarthDamage(e, damage);       
        Destroy(gameObject);
        yield return null;

    }
}
