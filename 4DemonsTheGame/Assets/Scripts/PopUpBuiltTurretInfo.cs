using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpBuiltTurretInfo : MonoBehaviour
{
    private Node target;

    public GameObject popUpUi;

    public GameObject rangeCircle;

    public TextMeshProUGUI damage;
    public TextMeshProUGUI range;
    public TextMeshProUGUI speed;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = new Vector2(target.transform.position.x, (target.transform.position.y + 0.10f));
        rangeCircle.transform.position = new Vector2(target.transform.position.x, (target.transform.position.y));
        rangeCircle.transform.localScale += new Vector3(_target.turret.GetComponent<Turret>().range, _target.turret.GetComponent<Turret>().range, 0); popUpUi.SetActive(true);
        rangeCircle.SetActive(true);

            damage.text = (_target.turret.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().bulletDamage * _target.turret.GetComponent<Turret>().damageBoost).ToString();
            range.text = _target.turret.GetComponent<Turret>().range.ToString();
            speed.text = _target.turret.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>().speed.ToString();
               
    }

    public void Hide()
    {
        rangeCircle.transform.localScale = new Vector3(1f, 1f, 1f);
        popUpUi.SetActive(false);
        rangeCircle.SetActive(false);
    }  
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
