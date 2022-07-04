using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int healthPoint = 100;

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;

        //if (healthPoint <= 0) Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}


