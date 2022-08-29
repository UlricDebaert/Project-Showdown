using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHeight : MonoBehaviour, IComparable<PlayerHeight>
{
    public int CompareTo(PlayerHeight other)
    {
        if (transform.position.y < other.transform.position.y)
        {
            return 1;
        }
        if (transform.position.y > other.transform.position.y)
        {
            return -1;
        }
        return 0;
    }

}
