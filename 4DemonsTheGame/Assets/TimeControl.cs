using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public Button toggle;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoubleTime()
    {
        if(Time.timeScale == 2)
        {
            Time.timeScale = 1;
            toggle.GetComponent<Image>().color = Color.white;
        }
        else
        {
            Time.timeScale = 2;
            toggle.GetComponent<Image>().color = new Color(255,270,0);
        }
        
    }
    



}
