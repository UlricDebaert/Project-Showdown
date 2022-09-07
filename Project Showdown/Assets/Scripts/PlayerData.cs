using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public CharacterSO character;
    public Image hpBar;

    public Color hitColor;
    Color defaultColor;

    public int playerID;
    public Color ownColor;

    public int killCount;
    public int deathCount;

    public bool canMove;
    public bool canShoot;
    public bool isDead;

    public int healthPoint;
    public GameObject ownGun;
    public GameObject ownPower;
    public GameObject ownPowerPrefab;

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
        GameManager.instance.AddToCamGroup(gameObject);

        UIManager.instance.playerPanels[playerID].UpdateKDText(killCount, deathCount);

        defaultColor = PC.playerSprite.color;
    }

    private void Update()
    {
        UpdateHpBar();
        if(isDead && PC.isGrounded)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            ownCollider.enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
        PC.playerSprite.color = hitColor;
        Invoke("ResetMaterial", 0.1f);
    }

    public void Death()
    {
        IncreaseDeathCount();
        Destroy(ownGun);
        //Destroy(ownPower);
        canShoot = false; canMove = false; isDead = true;
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

    void UpdateHpBar()
    {
        hpBar.fillAmount = (float)healthPoint / character.healthPoint;
    }

    void ResetMaterial()
    {
        PC.playerSprite.color = defaultColor;
    }
}
