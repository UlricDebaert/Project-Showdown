using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_HolyGrenade : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D ownCollider;
    Animator anim;
    [HideInInspector] public PlayerData PD;

    List<PlayerData> currentPlayerDatas;
    List<PlayerData> oldPlayerDatas;

    public float explosionRange;
    public float explosionTime;
    public float activeTime;

    public LayerMask playerLayer;

    bool exploded;
    bool back;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ownCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        currentPlayerDatas = new List<PlayerData>();
        oldPlayerDatas = new List<PlayerData>();

        exploded = false;
        back = false;
        anim.SetBool("back", false);
    }

    private void Update()
    {
        explosionTime -= Time.deltaTime;
        if (explosionTime <= 0.0f && !exploded) Explosion();
        anim.SetBool("exploded", back);

        if (exploded && !back) 
        { 
            activeTime -= Time.deltaTime; 
            if(activeTime <= 0.0f)
            {
                Disappear();
                back = true;
            }
        }

    }

    private void FixedUpdate()
    {
        if (exploded && !back)
        {
            Neutralization();
        }
    }

    public void Explosion()
    {
        transform.rotation = Quaternion.identity;
        rb.isKinematic = true;
        ownCollider.enabled = false;
        exploded = true;
        anim.Play("HolyGrenade_Explosion");
    }

    void Neutralization()
    {
        for (int i = 0; i < currentPlayerDatas.Count; i++)
        {
            currentPlayerDatas[i].canShoot = true;
            print("canShoot = true");
        }
        currentPlayerDatas.Clear();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), explosionRange);
        foreach (Collider2D hit in colliders)
        {
            print("hit detected");
            if ((playerLayer & (1 << hit.transform.gameObject.layer)) > 0)
            {
                print("hit added");
                currentPlayerDatas.Add(hit.GetComponent<PlayerData>());
            }
        }
        for (int i = 0; i < currentPlayerDatas.Count; i++)
        {
            currentPlayerDatas[i].canShoot = false;
            print("canShoot = false");
        }
    }

    public void Disappear()
    {

        currentPlayerDatas.Clear();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), explosionRange);
        foreach (Collider2D hit in colliders)
        {
            print("hit detected");
            if ((playerLayer & (1 << hit.transform.gameObject.layer)) > 0)
            {
                print("hit added");
                currentPlayerDatas.Add(hit.GetComponent<PlayerData>());
            }
        }
        for (int i = 0; i < currentPlayerDatas.Count; i++)
        {
            currentPlayerDatas[i].canShoot = true;
            print("canShoot = true");
        }

        anim.Play("HolyGrenade_Back");
    }

    public void DestroyItSelf()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
