using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerSpawn : MonoBehaviour
{
    Transform selfTransform;
    PlayerData PD;

    Rigidbody2D rb;
    BoxCollider2D ownCollider;

    PlayerInput inputs;

    public SpriteRenderer playerSprite;

    void Start()
    {
        print("spawn");
        PD = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
        ownCollider = GetComponent<BoxCollider2D>();
        inputs = GetComponent<PlayerInput>();
        selfTransform = GetComponent<Transform>();
        Spawn();
    }

    private void Update()
    {
        if(PD.isDead && inputs.actions["Spawn"].triggered)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        selfTransform.position = GameManager.instance.spawnPoints[Random.Range(0, GameManager.instance.spawnPoints.Length)].position;
        PD.healthPoint = PD.character.healthPoint;
        GameObject ownGun = Instantiate(PD.character.gun, PD.gameObject.transform);
        ownGun.transform.localPosition = PD.character.gunPos;
        PD.ownGun = ownGun;
        PD.canShoot = true; PD.canMove = true;
        rb.isKinematic = false;
        ownCollider.enabled = true;
        PD.isDead = false;
        playerSprite.flipX = false;
    }
}
