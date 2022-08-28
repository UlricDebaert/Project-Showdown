using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    PlayerSpawn PS;
    PlayerData PD;
    PlayerInput inputActions;

    int currentCharacterID;

    void Start()
    {
        PS = GetComponent<PlayerSpawn>();
        PD = GetComponent<PlayerData>();
        inputActions = GetComponentInParent<PlayerInput>();

        //StartCoroutine(LateStart()); //DONT ACTIVATE
    }

    private void Update()
    {
        if (inputActions.actions["SwitchCharacter"].triggered && PD.isDead)
        {
            SwitchCharacter();
        }
    }

    public void SwitchCharacter()
    {
        currentCharacterID += 1;
        if (currentCharacterID >= GameManager.instance.characters.Length) currentCharacterID = 0;
        PD.character = GameManager.instance.characters[currentCharacterID];
        UIManager.instance.playerPanels[PD.playerID].characterIcon.sprite = PD.character.characterIcon;
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);

        for (int i = 0; i < GameManager.instance.characters.Length; i++)
        {
            if (PD.character = GameManager.instance.characters[i]) currentCharacterID = i;
        }
    }
}
