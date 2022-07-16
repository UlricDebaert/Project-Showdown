using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public CharacterSO character;

    public int playerID;

    public int killCount;
    public int deathCount;

    public bool canMove;
    public bool canShoot;
    public bool isDead;

    [HideInInspector] public int healthPoint;
    [HideInInspector] public GameObject ownGun;
    [HideInInspector] public GameObject ownPower;

    Rigidbody2D rb;
    BoxCollider2D ownCollider;

    //Death Anim
    PlayerController PC;
    const string playerDeath = "Player_Death_Anim";

    private void Awake()
    {
        character = GameManager.instance.characters[Random.Range(0, GameManager.instance.characters.Length)];
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ownCollider = GetComponent<BoxCollider2D>();

        PC = GetComponent<PlayerController>();

        killCount = 0;
        deathCount = 0;

        playerID = GameManager.instance.playerCount;
        GameManager.instance.AddPlayer();

        UIManager.instance.playerPanels[playerID].UpdateKDText(killCount, deathCount);
    }

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
    }

    public void Death()
    {
        IncreaseDeathCount();
        Destroy(ownGun);
        //Destroy(ownPower);
        canShoot = false; canMove = false; isDead = true;
        rb.isKinematic = true;
        ownCollider.enabled = false;
        PC.ChangeAnimationState(playerDeath);
    }

    public void IncreaseKillCount()
    {
        killCount++;
        UIManager.instance.playerPanels[playerID].UpdateKDText(killCount, deathCount);
    }

    public void IncreaseDeathCount()
    {
        deathCount++;
        UIManager.instance.playerPanels[playerID].UpdateKDText(killCount, deathCount);
    }
}
