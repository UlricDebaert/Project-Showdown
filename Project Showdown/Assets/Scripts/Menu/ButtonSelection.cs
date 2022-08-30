using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour
{
    Button ownButton;

    public float sizeMultiplicator;
    public float transitionTime;
    float transitionTimer;

    bool isSelected;

    void Start()
    {
        ownButton = GetComponent<Button>();
        isSelected = false;
        transitionTimer = 0.0f;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (isSelected == false) transitionTimer = 0;
            isSelected = true;
        }
        else
        {
            if (isSelected == true) transitionTimer = 0;
            isSelected = false;
        }

        transitionTimer += Time.deltaTime;

        if (isSelected)
        {
            transform.localScale = new Vector3(Mathf.Lerp(1, sizeMultiplicator, transitionTimer/transitionTime), Mathf.Lerp(1, sizeMultiplicator, transitionTimer / transitionTime), 1);
        }

        if (!isSelected)
        {
            transform.localScale = new Vector3(Mathf.Lerp(sizeMultiplicator, 1, transitionTimer / transitionTime), Mathf.Lerp(sizeMultiplicator, 1, transitionTimer / transitionTime), 1);
        }
    }
}
