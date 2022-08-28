using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBetweenTwoPoint : MonoBehaviour
{
    LineRenderer lineRenderer;

    public Transform[] linePointTrasform;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        for (int i = 0; i < linePointTrasform.Length; i++)
        {
            //print("set" + linePointTrasform[i].position);
            lineRenderer.SetPosition(i, linePointTrasform[i].position);
        }
    }
}
