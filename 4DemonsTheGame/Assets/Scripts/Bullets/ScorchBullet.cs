using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorchBullet : AbstractBullet
{
    public override IEnumerator bulletSpecialDamage(float damage)
    {
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);
        float pushBack = damage / 100f;

        
        Coroutine fireDamage = StartCoroutine(FireDamage(e,damage));
        Coroutine windDamage = StartCoroutine(WindDamage(e,pushBack));
        while (fireDamage != null && windDamage != null)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    //IEnumerator FireDamage(Enemy _enemy, float _damage)
    //{       
    //        for (int i = 0; i < 5; i++)
    //        {
    //            if (_enemy == null)
    //            {
    //                yield break;
    //            }

    //            _enemy.TakeDamage(_damage / 10f);
    //            yield return new WaitForSecondsRealtime(.1f);
    //        }             
    //}

    //IEnumerator WindDamage(Enemy _enemy, float _pushBackPower)
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (_enemy == null)
    //        {
    //            yield break;
    //        }
    //        Vector2 moveTo = new Vector2(_enemy.transform.position.x - _pushBackPower, _enemy.transform.position.y);
    //        _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, moveTo, speed * Time.deltaTime);
    //        yield return null;
    //    }
    //}


}
