using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SP_BearTrapHolder : MonoBehaviour
{
    public GameObject bearTrapPrefab;
    public Transform placementPos;

    public float reloadTime;
    float reloadTimer;

    bool canThrow;

    PlayerData PD;
    PlayerController PC;

    PlayerInput playerInput;
    InputAction specialPowerInput;
    float inputHold;
    public float minimalInputSensitivity = 0.5f;

    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        PD = GetComponentInParent<PlayerData>();
        PC = GetComponentInParent<PlayerController>();
        specialPowerInput = playerInput.actions["SpecialPower"];
        canThrow = true;
    }

    private void Update()
    {
        specialPowerInput.performed += ctx => inputHold = ctx.ReadValue<float>();
        specialPowerInput.canceled += ctx => inputHold = ctx.ReadValue<float>();

        if (inputHold > .5f && !PD.isDead && canThrow && PC.isGrounded && PC.gameObject.transform.parent == null)
        {
            PlaceTrap();
            canThrow = false;
            reloadTimer = reloadTime;
        }

        if (reloadTimer >= 0.0f) Timer();
        else canThrow = true;

        UpdateUIGraphics();
    }

    void PlaceTrap()
    {
        GameObject trap = Instantiate(bearTrapPrefab, placementPos.position, placementPos.rotation);
        trap.GetComponent<SP_BearTrap>().PD = PD;
    }

    void Timer()
    {
        reloadTimer -= Time.deltaTime;
    }

    void UpdateUIGraphics()
    {
        UIManager.instance.playerPanels[PD.playerID].SPLoadingIcon.fillAmount = reloadTimer / reloadTime;
    }
}
