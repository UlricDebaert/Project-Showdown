using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask wallLayers;
    public LayerMask dummyLayers;

    [HideInInspector] public int bulletDamage;
    [HideInInspector] public int maxTargetsPenetration;
    [HideInInspector] int targetPenetrated;
    [HideInInspector] public float penetrationMultiplier;
    //[HideInInspector] public float knockbackOnTarget;
    [HideInInspector] public bool destroyed;
    public PlayerData selfPlayer;

    Rigidbody2D rb;
    Animator anim;

    public float destroyAnimDelay;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        destroyed = false;
        if(anim!=null)
        anim.SetBool("destroyed", destroyed);

        targetPenetrated = 0;
    }

    private void Update()
    {
        //if (destroyed)
        //{
        //    rb.isKinematic = true;
        //    rb.velocity = new Vector2(0, 0); 
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((wallLayers & (1 << collision.transform.gameObject.layer)) > 0)
        {
            rb.isKinematic = true;
            rb.velocity = new Vector2(0, 0);
            transform.position = collision.ClosestPoint(transform.position);
            DestroyItself();
        }
        if ((dummyLayers & (1 << collision.transform.gameObject.layer)) > 0 && collision.gameObject != selfPlayer.gameObject)
        {
            collision.GetComponent<PlayerData>().TakeDamage(Mathf.RoundToInt(bulletDamage / (1 + targetPenetrated * penetrationMultiplier)));
            if (collision.GetComponent<PlayerData>().healthPoint <= 0 && !collision.GetComponent<PlayerData>().isDead)
            {
                selfPlayer.IncreaseKillCount();
                collision.GetComponent<PlayerData>().Death();
            }

            if (targetPenetrated >= maxTargetsPenetration) DestroyItself();
            else targetPenetrated++;
        }
    }

    public void DestroyItself()
    {
        destroyed = true;
        if (anim != null)
            anim.SetBool("destroyed", destroyed);
        Destroy(gameObject, destroyAnimDelay);
    }
}
