using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public Button toggle;
    public float TimeState;
    public static TimeControl instance;

    void Awake()
    {
        //each time we start the game there's only one TimeController and this variable can be accessed by anywhere
        if (instance != null)
        {
            Debug.LogError("More than one GameController in scene");
        }
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        TimeState = Time.timeScale;
    }
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

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
    



}
