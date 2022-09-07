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

    List<Transform> possibleSpawnPos;

    void Start()
    {
        print("spawn");
        PD = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
        ownCollider = GetComponent<BoxCollider2D>();
        inputs = GetComponent<PlayerInput>();
        selfTransform = GetComponent<Transform>();
        GameManager.instance.playerList.Add(PD);
        GameManager.instance.playerHeights.Add(GetComponent<PlayerHeight>());
        Spawn();

        if (PD.character.specialPowerPrefab != null)
        {
            GameObject ownPower = Instantiate(PD.character.specialPowerPrefab, PD.gameObject.transform);
            PD.ownPower = ownPower;
            PD.ownPowerPrefab = PD.character.specialPowerPrefab;
        }

        if(GameManager.instance.playersPossibleColor.Count > 0)
        {
            SetColor();
        }
        else
        {
            GameManager.instance.playersPossibleColor = GameManager.instance.playersColor;
            SetColor();
        }
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
        //selfTransform.position = GameManager.instance.spawnPoints[Random.Range(0, GameManager.instance.spawnPoints.Count-1)].position;

        possibleSpawnPos = GameManager.instance.spawnPoints;

        //possibleSpawnPos[Random.Range(0, GameManager.instance.spawnPoints.Count - 1)].GetComponent<Spawn>().DetectPlayer()

        int spawnPosIndex = Random.Range(0, possibleSpawnPos.Count);

        //print(spawnPosIndex);
        //print(possibleSpawnPos[spawnPosIndex].GetComponent<Spawn>().DetectPlayer());

        if (possibleSpawnPos[spawnPosIndex].GetComponent<Spawn>().DetectPlayer())
        {
            while (possibleSpawnPos[spawnPosIndex].GetComponent<Spawn>().DetectPlayer())
            {

                if (possibleSpawnPos.Count > 0)
                {
                    possibleSpawnPos.RemoveAt(spawnPosIndex);
                    spawnPosIndex = Random.Range(0, possibleSpawnPos.Count - 1);
                }
                else
                {
                    selfTransform.position = GameManager.instance.spawnPoints[Random.Range(0, GameManager.instance.spawnPoints.Count - 1)].position;
                    break;
                }
            }
            if (possibleSpawnPos.Count > 0) selfTransform.position = possibleSpawnPos[spawnPosIndex].position;
        }
        else
        {
            selfTransform.position = possibleSpawnPos[spawnPosIndex].position;
        }


        PD.healthPoint = PD.character.healthPoint;

        GameObject ownGun = Instantiate(PD.character.gun, PD.gameObject.transform);
        ownGun.transform.localPosition = PD.character.gunPos;
        PD.ownGun = ownGun;

        PD.canShoot = true; PD.canMove = true;

        if(PD.ownPowerPrefab != null && PD.ownPowerPrefab != PD.character.specialPowerPrefab)
        {
            //print("spawn new power");
            Destroy(PD.ownPower);
            GameObject ownPower = Instantiate(PD.character.specialPowerPrefab, PD.gameObject.transform);
            PD.ownPower = ownPower;
            PD.ownPowerPrefab = PD.character.specialPowerPrefab;
        }

        UIManager.instance.playerPanels[PD.playerID].SPIcon.sprite = PD.character.powerIcon;
        UIManager.instance.playerPanels[PD.playerID].SPLoadingIcon.sprite = PD.character.powerIcon;
        
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

        anim.runtimeAnimatorController = animOverrideController;

        animOverrideController["Player_Idle_Anim"] = PD.character.idleAnim;
        animOverrideController["Player_Jump_Anim"] = PD.character.jumpAnim;
        animOverrideController["Player_Walk_Anim"] = PD.character.walkAnim;
        animOverrideController["Player_WalkBack_Anim"] = PD.character.walkBackAnim;
        animOverrideController["Player_Crouch_Anim"] = PD.character.crouchAnim;
        animOverrideController["Player_Death_Anim"] = PD.character.deathAnim;
        animOverrideController["Player_SpecialPower_Anim"] = PD.character.specialPowerAnim;

        StartCoroutine(LateStart());
    }

    void SetColor()
    {
        int colorIndex = Random.Range(0, GameManager.instance.playersPossibleColor.Count);
        PD.ownColor = GameManager.instance.playersPossibleColor[colorIndex];
        GameManager.instance.playersPossibleColor.RemoveAt(colorIndex);

        UIManager.instance.playerPanels[PD.playerID].playerColor.color = PD.ownColor;
        PD.hpBar.color = PD.ownColor;
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);

        //UIManager.instance.playerPanels[PD.playerID].characterName.text = PD.character.characterName;
        UIManager.instance.playerPanels[PD.playerID].characterName.text = "Player " + (PD.playerID+1).ToString();
        UIManager.instance.playerPanels[PD.playerID].characterIcon.sprite = PD.character.characterIcon;
    }
}
