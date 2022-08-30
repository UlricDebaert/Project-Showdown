using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_RandomCharacter : MonoBehaviour
{
    public int id;

    public float speed;
    public float lifeTime;

    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("id", id);
    }


    void Update()
    {
        transform.position = new Vector3 (transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        if (lifeTime > 0) lifeTime -= Time.deltaTime;
        else Destroy(gameObject);
    }
}
