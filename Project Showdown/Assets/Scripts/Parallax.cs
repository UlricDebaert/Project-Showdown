using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform camTransform;

    public float scrollSpeed;


    void Update()
    {
        transform.position = new Vector3(camTransform.position.x * scrollSpeed, transform.position.y);
    }
}
