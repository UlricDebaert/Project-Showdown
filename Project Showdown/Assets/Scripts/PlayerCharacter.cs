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
    }

    private void Update()
    {
        if (inputActions.actions["SwitchCharacter"].triggered)// && PD.isDead)
        {
            SwitchCharacter();
        }
    }

    public void SwitchCharacter()
    {
        currentCharacterID += 1;
        if (currentCharacterID >= GameManager.instance.characters.Length) currentCharacterID = 0;
        PD.character = GameManager.instance.characters[currentCharacterID];
    }
}
