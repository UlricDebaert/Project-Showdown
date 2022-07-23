using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SP_Holster : MonoBehaviour
{
    PlayerData PD;

    PlayerInput playerInput;
    InputAction specialPowerInput;
    float inputHold;

    public GameObject gunPrefab;
    public Vector3 gunPos;
    GameObject gun;

    public float drawGunTime;
    float drawGunTimer;

    public float storeGunTime;

    public float reloadTime;
    float reloadTimer;

    bool canDraw;

    void Start()
    {
        PD = GetComponentInParent<PlayerData>();
        playerInput = GetComponentInParent<PlayerInput>();
        specialPowerInput = playerInput.actions["SpecialPower"];
    }


    void Update()
    {
        specialPowerInput.performed += ctx => inputHold = ctx.ReadValue<float>();
        specialPowerInput.canceled += ctx => inputHold = ctx.ReadValue<float>();

        if (inputHold > .5f && !PD.isDead && canDraw)
        {
            Draw();
            canDraw = false;
            reloadTimer = reloadTime;
            drawGunTimer = drawGunTime;
        }

        if((inputHold < .5f || PD.isDead || drawGunTimer <= 0.0f) && gun != null)
        {
            Store();
        }

        if (!canDraw)
        {
            reloadTimer -= Time.deltaTime;
            if(reloadTimer <= 0.0f)
            {
                canDraw = true;
            }
        }

        if(drawGunTimer > 0.0f) drawGunTimer -= Time.deltaTime;

        UpdateUIGraphics();
    }

    public void Draw()
    {
        PD.ownGun.SetActive(false);

        GameObject ownGun = Instantiate(gunPrefab, PD.gameObject.transform);
        ownGun.transform.localPosition = gunPos;
        ownGun.transform.localRotation = PD.ownGun.transform.localRotation;
        ownGun.GetComponent<Gun>().isHolsterWeapon = true;
        gun = ownGun;
    }

    public void Store()
    {
        StartCoroutine(DestroyGun());
    }

    IEnumerator DestroyGun()
    {
        yield return new WaitForSeconds(storeGunTime);

        PD.ownGun.SetActive(true);
        Destroy(gun);
        UIManager.instance.playerPanels[PD.playerID].loadingIcon.fillAmount = 0.0f;
        PD.ownGun.GetComponent<Gun>().UpdateAmmoCount();
    }

    void UpdateUIGraphics()
    {
        UIManager.instance.playerPanels[PD.playerID].SPLoadingIcon.fillAmount = reloadTimer / reloadTime;
    }
}
