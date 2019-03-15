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

    public void SetTarget(Transform transform)
    {

        this.transform.position = new Vector2(transform.position.x, (transform.position.y + 0.75f));
        
    }

    public void setStats(string name)
    {
        if(name == "StandardTower")
        {
            damage.text = shop.GetStandardTower().prefab.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().damage.ToString();
            range.text = shop.GetStandardTower().prefab.GetComponent<Turret>().range.ToString();
            speed.text = shop.GetStandardTower().prefab.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().speed.ToString();
        }
        if(name == "MissileTower")
        {
            damage.text = shop.GetMissileTower().prefab.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().damage.ToString();
            range.text = shop.GetMissileTower().prefab.GetComponent<Turret>().range.ToString();
            speed.text = shop.GetMissileTower().prefab.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().speed.ToString();
        }
       
    }

}
