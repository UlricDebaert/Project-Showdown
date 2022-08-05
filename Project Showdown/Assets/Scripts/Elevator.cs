using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform elevator;
    [Space]
    public List<Transform> waypoint;
    int target;
    [Space]
    public float speed;
    public float freezeSpeed;
    float freezeSpeedTimer;

    void Start()
    {
        elevator.transform.position = waypoint[0].position;
        target = 1;
        freezeSpeedTimer = freezeSpeed;
    }


    void Update()
    {
        elevator.position = Vector3.MoveTowards(elevator.position, waypoint[target].position, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(elevator.position == waypoint[target].position)
        {
            freezeSpeedTimer -= Time.fixedDeltaTime;

            if(freezeSpeedTimer <= 0.0f)
            {
                if(target == waypoint.Count-1)
                {
                    target = 0;
                    freezeSpeedTimer = freezeSpeed;
                }
                else
                {
                    target += 1;
                    freezeSpeedTimer = freezeSpeed;
                }
            }
        }
    }


}
