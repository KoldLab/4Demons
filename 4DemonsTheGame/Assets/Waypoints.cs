using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    void Awake()
    {
        points = new Transform[transform.childCount];
        Debug.Log(transform.childCount);
        for (int i = 1; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }


    }
}
