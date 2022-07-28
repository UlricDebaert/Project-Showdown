using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_BearTrap : MonoBehaviour
{
    Collider2D ownCollider;
    Animator anim;
    [HideInInspector] public PlayerData PD;

    public int damage;

    public float activeTime;
    public float disappearTime;

    public LayerMask playerLayer;
    public LayerMask bulletLayer;

    bool isTriggered;

    void Start()
    {
        ownCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        isTriggered = false;
        anim.SetBool("isTriggered", isTriggered);
    }

    private void Update()
    {
        activeTime -= Time.deltaTime;
        if (activeTime <= 0.0f && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(Disappear(disappearTime));
        }
    }

    IEnumerator Disappear(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((bulletLayer & (1 << collision.transform.gameObject.layer)) > 0 && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(Disappear(disappearTime));
        }

        if ((playerLayer & (1 << collision.transform.gameObject.layer)) > 0 && !isTriggered && collision.gameObject != PD.gameObject)
        {
            collision.GetComponent<PlayerData>().TakeDamage(damage);
            if (collision.GetComponent<PlayerData>().healthPoint <= 0)
            {
                PD.IncreaseKillCount();
                collision.GetComponent<PlayerData>().Death();
            }

            isTriggered = true;
            StartCoroutine(Disappear(disappearTime));
        }
    }
}
