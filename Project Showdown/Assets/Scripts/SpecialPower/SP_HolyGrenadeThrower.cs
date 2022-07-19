using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SP_HolyGrenadeThrower : MonoBehaviour
{
    public GameObject holyGrenadePrefab;

    public float throwPower;
    public float reloadTime;
    float reloadTimer;

    bool canThrow;

    PlayerData PD;

    PlayerInput playerInput;
    InputAction specialPowerInput;
    InputAction aimInput;
    float inputHold;
    public float minimalInputSensitivity = 0.5f;

    Vector2 lookPosition;

    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        PD = GetComponentInParent<PlayerData>();
        specialPowerInput = playerInput.actions["SpecialPower"];
        aimInput = playerInput.actions["Aim"];
        canThrow = true;
    }

    private void Update()
    {
        Aim();

        specialPowerInput.performed += ctx => inputHold = ctx.ReadValue<float>();
        specialPowerInput.canceled += ctx => inputHold = ctx.ReadValue<float>();

        if (inputHold > .5f && !PD.isDead && canThrow)
        {
            ThrowDynamite();
            canThrow = false;
            reloadTimer = reloadTime;
        }

        if (reloadTimer >= 0.0f) Timer();
        else canThrow = true;

        UpdateUIGraphics();
    }

    void Timer()
    {
        reloadTimer -= Time.deltaTime;
    }

    void Aim()
    {
        lookPosition = aimInput.ReadValue<Vector2>();
        if (lookPosition.x > minimalInputSensitivity || lookPosition.y > minimalInputSensitivity || lookPosition.x < -minimalInputSensitivity || lookPosition.y < -minimalInputSensitivity)
        {
            transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(lookPosition.x, lookPosition.y) * -180 / Mathf.PI + 90f);
        }
    }

    void ThrowDynamite()
    {
        GameObject dynamite = Instantiate(holyGrenadePrefab, gameObject.transform.position, gameObject.transform.rotation);
        dynamite.GetComponent<Rigidbody2D>().AddForce(throwPower * gameObject.transform.right.normalized, ForceMode2D.Impulse);
        dynamite.GetComponent<SP_HolyGrenade>().PD = PD;
    }

    void UpdateUIGraphics()
    {
        UIManager.instance.playerPanels[PD.playerID].SPLoadingIcon.fillAmount = reloadTimer / reloadTime;
    }
}
