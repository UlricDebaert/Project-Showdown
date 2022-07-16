using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Dynamite : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D ownCollider;
    Animator anim;

    public int damage;
    public AnimationCurve damageFalloff;

    public float explosionRange;
    public float explosionTime;
    public float disappearTime;

    public LayerMask playerLayer;
    public LayerMask groundLayer;

    bool exploded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ownCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        exploded = false;
    }

    private void Update()
    {
        explosionTime -= Time.deltaTime;
        if (explosionTime <= 0.0f && !exploded) Explosion();
        anim.SetBool("exploded", exploded);
    }

    public void Explosion()
    {
        rb.isKinematic = true;
        ownCollider.enabled = false;
        exploded = true;

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider hit in colliders)
        {
            if((playerLayer & (1 << hit.transform.gameObject.layer)) > 0)
            {
                RaycastHit2D hitObject = Physics2D.Raycast(transform.position, hit.transform.position, Vector2.Distance(transform.position, hit.transform.position), groundLayer);
                if(hitObject.collider == null)
                {
                    int damageApplyied = Mathf.RoundToInt(damage * damageFalloff.Evaluate(Vector2.Distance(transform.position, hit.transform.position) / explosionRange));
                    hit.GetComponent<PlayerData>().TakeDamage(damageApplyied);
                }
            }
        }

        StartCoroutine(Disappear(disappearTime));
    }

    IEnumerator Disappear(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
