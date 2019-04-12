using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PopUpTowerInfo : MonoBehaviour
{
    public Shop shop;

    public TextMeshProUGUI damage;
    public TextMeshProUGUI range;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI effect;

    public void SetTarget(Transform transform)
    {

        this.transform.position = new Vector2(transform.position.x, (transform.position.y + 1f));
        
    }

    public void setStats(string name)
    {
        Turret tower;

        if (name == "FireTower")
        {
            tower = shop.towersBlueprintTable[0].prefab.GetComponent<Turret>();
            Debug.Log("Effect :" + tower.bulletPrefab.GetComponent<AbstractBullet>().bulletEffect);
            effect.text = tower.bulletPrefab.GetComponent<AbstractBullet>().bulletEffect.ToString();
            damage.text = tower.damage.ToString();
            range.text = tower.range.ToString();
            speed.text = tower.bulletPrefab.GetComponent<AbstractBullet>().speed.ToString();

        }
        if (name == "WindTower")
        {
            tower = shop.towersBlueprintTable[1].prefab.GetComponent<Turret>();
            Debug.Log("Effect :" + tower.bulletPrefab.GetComponent<AbstractBullet>().bulletEffect);
            effect.text = tower.bulletPrefab.GetComponent<AbstractBullet>().bulletEffect.ToString();
            damage.text = tower.damage.ToString();
            range.text = tower.range.ToString();
            speed.text = tower.bulletPrefab.GetComponent<AbstractBullet>().speed.ToString();
        }
        if (name == "LightningTower")
        {
            tower = shop.towersBlueprintTable[2].prefab.GetComponent<Turret>();
            Debug.Log("Effect :" + tower.bulletPrefab.GetComponent<AbstractBullet>().bulletEffect);
            effect.text = tower.bulletPrefab.GetComponent<AbstractBullet>().bulletEffect.ToString();
            damage.text = tower.damage.ToString();
            range.text = tower.range.ToString();
            speed.text = tower.bulletPrefab.GetComponent<AbstractBullet>().speed.ToString();
        }
        if (name == "EarthTower")
        {
            tower = shop.towersBlueprintTable[3].prefab.GetComponent<Turret>();
            Debug.Log("Effect :" + tower.bulletPrefab.GetComponent<AbstractBullet>().bulletEffect);
            effect.text = tower.bulletPrefab.GetComponent<AbstractBullet>().bulletEffect.ToString();
            damage.text = tower.damage.ToString();
            range.text = tower.range.ToString();
            speed.text = tower.bulletPrefab.GetComponent<AbstractBullet>().speed.ToString();
        }
        if (name == "WaterTower")
        {
            tower = shop.towersBlueprintTable[4].prefab.GetComponent<Turret>();
            Debug.Log("Effect :" + tower.bulletPrefab.GetComponent<AbstractBullet>().bulletEffect);
            effect.text = tower.bulletPrefab.GetComponent<AbstractBullet>().bulletEffect.ToString();
            damage.text = tower.damage.ToString();
            range.text = tower.range.ToString();
            speed.text = tower.bulletPrefab.GetComponent<AbstractBullet>().speed.ToString();
        }
        else
            return;
        
              

    }

}
