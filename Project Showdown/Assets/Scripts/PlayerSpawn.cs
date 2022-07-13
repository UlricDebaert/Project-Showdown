using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.InputSystem;
using UnityEditor;


public class PlayerSpawn : MonoBehaviour
{
    Transform selfTransform;
    PlayerData PD;

    Rigidbody2D rb;
    BoxCollider2D ownCollider;

    PlayerInput inputs;

    public SpriteRenderer playerSprite;
    public Animator anim;
    AnimatorOverrideController animOverrideController;
    public AnimatorController originalAnimationController;

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

        if (PD.character.specialPowerPrefab != null)
        {
            GameObject ownPower = Instantiate(PD.character.specialPowerPrefab, PD.gameObject.transform);
            PD.ownPower = ownPower;
        }

        GameObject ownGun = Instantiate(PD.character.gun, PD.gameObject.transform);
        ownGun.transform.localPosition = PD.character.gunPos;
        PD.ownGun = ownGun;

        PD.canShoot = true; PD.canMove = true;

        rb.isKinematic = false;
        ownCollider.enabled = true;

        PD.isDead = false;
        playerSprite.flipX = false;

        UpdatePlayerGraphics();
    }

    public void UpdatePlayerGraphics()
    {
        playerSprite.sprite = PD.character.baseSprite;

        if (originalAnimationController != null)
            animOverrideController = new AnimatorOverrideController(AssetDatabase.LoadAssetAtPath<AnimatorController>(AssetDatabase.GetAssetPath(originalAnimationController)));

        if (anim.runtimeAnimatorController != null)
            anim.runtimeAnimatorController = animOverrideController;

        animOverrideController["Player_Idle_Anim"] = PD.character.idleAnim;
        animOverrideController["Player_Jump_Anim"] = PD.character.jumpAnim;
        animOverrideController["Player_Walk_Anim"] = PD.character.walkAnim;
    }
}
