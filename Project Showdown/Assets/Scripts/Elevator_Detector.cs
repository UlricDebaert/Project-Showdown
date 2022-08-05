using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Detector : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject elevator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((playerLayer & (1 << collision.transform.gameObject.layer)) > 0)
        {
            collision.gameObject.transform.parent = elevator.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if((playerLayer & (1 << collision.transform.gameObject.layer)) > 0)
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
