using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float detectionRadius;

    public LayerMask playerLayer;
    public LayerMask groundLayer;


    public bool DetectPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), detectionRadius);
        foreach (Collider2D hit in colliders)
        {
            if ((playerLayer & (1 << hit.transform.gameObject.layer)) > 0)
            {
                if (!Physics2D.Linecast(transform.position, hit.transform.position, groundLayer))
                {
                    //print(hit.GetComponent<PlayerData>().character.ToString());
                    return true;
                }
            }
        }

        return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
