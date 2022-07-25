using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SP_Teleportation : MonoBehaviour
{
    PlayerData PD;

    PlayerInput playerInput;
    InputAction specialPowerInput;
    InputAction aimInput;
    float inputHold;
    public float minimalInputSensitivity = 0.5f;
    Vector3 lookPosition;

    public float teleportTime;
    public float teleportDistance;

    public float reloadTime;
    float reloadTimer;
    bool canTeleport;

    const string playerSpecialPower = "Player_SpecialPower_Anim";

    void Start()
    {
        PD = GetComponentInParent<PlayerData>();
        playerInput = GetComponentInParent<PlayerInput>();
        specialPowerInput = playerInput.actions["SpecialPower"];
        aimInput = playerInput.actions["Aim"];
        canTeleport = true;
    }


    void Update()
    {
        specialPowerInput.performed += ctx => inputHold = ctx.ReadValue<float>();
        specialPowerInput.canceled += ctx => inputHold = ctx.ReadValue<float>();

        if (inputHold > .5f && !PD.isDead && canTeleport)
        {
            StartCoroutine(Teleport());
            canTeleport = false;
            reloadTimer = reloadTime;
        }

        if (!canTeleport)
        {
            reloadTimer -= Time.deltaTime;
            if(reloadTimer <= 0.0f)
            {
                canTeleport = true;
            } 
        }

        Aim();
        UpdateUIGraphics();
    }

    IEnumerator Teleport()
    {
        PD.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        PD.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        PD.canMove = false;
        PD.gameObject.GetComponent<PlayerController>().ChangeAnimationState(playerSpecialPower);

        Vector3 newPos = PD.gameObject.transform.position + lookPosition.normalized * teleportDistance;

        yield return new WaitForSeconds(teleportTime);
        PD.gameObject.transform.position = newPos;

        yield return new WaitForSeconds(teleportTime);
        PD.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        PD.canMove = true;
    }

    void Aim()
    {
        lookPosition = aimInput.ReadValue<Vector2>();
        //if (lookPosition.x > minimalInputSensitivity || lookPosition.y > minimalInputSensitivity || lookPosition.x < -minimalInputSensitivity || lookPosition.y < -minimalInputSensitivity)
        //{
        //    transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(lookPosition.x, lookPosition.y) * -180 / Mathf.PI + 90f);
        //}
    }

    void UpdateUIGraphics()
    {
        UIManager.instance.playerPanels[PD.playerID].SPLoadingIcon.fillAmount = reloadTimer / reloadTime;
    }
}
