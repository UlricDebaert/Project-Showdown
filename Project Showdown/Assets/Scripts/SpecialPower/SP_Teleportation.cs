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

    [Header("Teleport Stats")]
    public float teleportTime;
    public float teleportDistance;

    [Header("Reload")]
    public float reloadTime;
    float reloadTimer;
    bool canTeleport;

    [Header("Teleport Check")]
    public Vector3[] posCheckedDir;
    public float posCheckedDist;
    public float posCheckedRadius;
    public LayerMask groundLayer;

    [Header("Teleport Sound")]
    public AudioClip teleportSound;
    AudioSource audioSource;

    const string playerSpecialPower = "Player_SpecialPower_Anim";

    void Start()
    {
        PD = GetComponentInParent<PlayerData>();
        playerInput = GetComponentInParent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(teleportSound);
        PD.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        PD.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        PD.canMove = false;
        PD.gameObject.GetComponent<PlayerController>().ChangeAnimationState(playerSpecialPower);
        PD.gameObject.GetComponent<PlayerController>().animLock = true;

        Vector3 newPos = PD.gameObject.transform.position + lookPosition.normalized * teleportDistance;
        //print("get input");

        yield return new WaitForSeconds(teleportTime);
        PD.gameObject.transform.position = TeleportPlace(newPos);
        //print("tp");

        //yield return new WaitForSeconds(teleportTime);
        PD.gameObject.GetComponent<PlayerController>().animLock = false;
        PD.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        PD.canMove = true;
        //print("release");
    }


    public Vector3 TeleportPlace(Vector3 newPos)
    {
        for (int i = 0; i < posCheckedDir.Length; i++)
        {
            if(!Physics2D.OverlapCircle(newPos + posCheckedDir[i].normalized * posCheckedDist, posCheckedRadius, groundLayer)) 
                return newPos + posCheckedDir[i].normalized * posCheckedDist;
        }

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, newPos, Vector3.Distance(transform.position, newPos));
        //print(new Vector2(hit.point.x - newPos.x, hit.point.y - newPos.y).normalized/2);

        if(hit.point.x - newPos.x >= 0 ) return hit.point + new Vector2(hit.point.x - newPos.x, hit.point.y - newPos.y).normalized / 2;
        if(hit.point.x - newPos.x < 0 ) return hit.point + new Vector2(newPos.x - hit.point.x, newPos.y - hit.point.y).normalized / 2;

        print("/!/ Teleport return null");
        return Vector3.zero;
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
