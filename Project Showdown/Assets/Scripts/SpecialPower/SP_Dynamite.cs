using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Dynamite : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D ownCollider;
    Animator anim;
    [HideInInspector] public PlayerData PD;

    public int damage;
    public AnimationCurve damageFalloff;

    public float explosionRange;
    public float explosionSize;
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
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(explosionSize, explosionSize, explosionSize);
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.freezeRotation = true;
        ownCollider.enabled = false;
        exploded = true;

        //Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), explosionRange);
        foreach (Collider2D hit in colliders)
        {
            //print("BOOM");
            if((playerLayer & (1 << hit.transform.gameObject.layer)) > 0)
            {
                //print("BOOM PLAYER");
                RaycastHit2D hitObject = Physics2D.Raycast(transform.position, hit.transform.position, Vector2.Distance(transform.position, hit.transform.position), groundLayer);
                if(hitObject.collider != null)
                {
                    //print("BOOM DAMAGE");
                    int damageApplyied = Mathf.RoundToInt(damage * damageFalloff.Evaluate(Vector2.Distance(transform.position, hit.transform.position) / explosionRange));
                    hit.GetComponent<PlayerData>().TakeDamage(damageApplyied);
                    if (hit.GetComponent<PlayerData>().healthPoint <= 0 && hit.gameObject != PD.gameObject) PD.IncreaseKillCount();
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
