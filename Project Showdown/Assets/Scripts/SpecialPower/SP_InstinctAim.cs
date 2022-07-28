using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SP_InstinctAim : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public float maxDistance;

    PlayerData PD;

    PlayerInput playerInput;
    InputAction specialPowerInput;
    InputAction aimInput;
    float inputHold;
    public float minimalInputSensitivity = 0.5f;

    Vector2 lookPosition;

    public LayerMask hittableLayers;


    void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        PD = GetComponentInParent<PlayerData>();
        specialPowerInput = playerInput.actions["SpecialPower"];
        aimInput = playerInput.actions["Aim"];
        transform.localPosition = PD.character.gunPos;
    }


    void Update()
    {
        specialPowerInput.performed += ctx => inputHold = ctx.ReadValue<float>();
        specialPowerInput.canceled += ctx => inputHold = ctx.ReadValue<float>();

        Aim();
        if (inputHold > .5f && !PD.isDead)
        {
            DrawAim();
        }
        if (inputHold < .5f || PD.isDead)
        {
            DesactivateAim();
        }
    }

    void DrawAim()
    {
        RaycastHit2D hitObject = Physics2D.Raycast(transform.position, lookPosition.normalized, maxDistance, hittableLayers);
        if (lookPosition.magnitude > minimalInputSensitivity)
        {
            if (hitObject.point != Vector2.zero) lineRenderer.SetPosition(1, hitObject.point - new Vector2(transform.position.x, transform.position.y));
            else lineRenderer.SetPosition(1, new Vector2(lookPosition.normalized.x * maxDistance, lookPosition.normalized.y * maxDistance));
        }
    }

    void DesactivateAim()
    {
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    void Aim()
    {
        lookPosition = aimInput.ReadValue<Vector2>();
        //if (lookPosition.x > minimalInputSensitivity || lookPosition.y > minimalInputSensitivity || lookPosition.x < -minimalInputSensitivity || lookPosition.y < -minimalInputSensitivity)
        //{
        //    transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(lookPosition.x, lookPosition.y) * -180 / Mathf.PI + 90f);
        //}
    }
}
