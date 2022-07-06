using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    PlayerSpawn PS;
    PlayerInput inputActions;


    void Start()
    {
        PS = GetComponent<PlayerSpawn>();
        inputActions = GetComponentInParent<PlayerInput>();
    }

    private void Update()
    {
        if (inputActions.actions["SwitchCharacter"].triggered)
        {
            SwitchCharacter();
        }
    }

    public void SwitchCharacter()
    {
        print("switch");
    }
}
