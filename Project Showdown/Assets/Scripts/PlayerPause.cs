using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPause : MonoBehaviour
{
    PlayerInput inputActions;
    InputAction pauseInput;


    void Start()
    {
        inputActions = GetComponentInParent<PlayerInput>();
        pauseInput = inputActions.actions["Pause"];
    }


    void Update()
    {
        if (pauseInput.triggered)
        {
            switch (GameManager.gameIsPaused)
            {
                case true:

                    print("resume by start");
                    GameManager.gameIsPaused = false;
                    Time.timeScale = 1.0f;
                    UIManager.instance.resumeEvent.Invoke();
                    break;

                case false :

                    print("show pause");
                    GameManager.gameIsPaused = true;
                    Time.timeScale = 0.0f;
                    UIManager.instance.pauseEvent.Invoke();
                    break;

            }

        }
    }
}
