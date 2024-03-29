// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""aaf75a7e-90cf-4c2a-96e7-ac340e585ab7"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f0e6d39c-1c7a-40a2-822d-d9a716d24abd"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Value"",
                    ""id"": ""9d09fc6e-ae82-4739-8106-6f65aab76e48"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""13a9dd9c-fbc0-4757-86ce-8746174250c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""AutoFire"",
                    ""type"": ""Button"",
                    ""id"": ""c744e0c0-8aa8-47de-b8e0-ccae78f91046"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SemiFire"",
                    ""type"": ""Button"",
                    ""id"": ""15968da1-0a43-4137-b4f7-300f6460c82d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cbf7837e-0f71-4c2b-92a6-78264fe98651"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchCharacter"",
                    ""type"": ""Button"",
                    ""id"": ""7aa9ceed-583e-4cd7-b2ca-ef60ee4daf0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Spawn"",
                    ""type"": ""Button"",
                    ""id"": ""bdaafa9b-a995-4f0e-b261-cc76b176df78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""510eb0ed-8e63-458e-8cd0-bed8ec3f1b04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SpecialPower"",
                    ""type"": ""Button"",
                    ""id"": ""6acd35c8-6ab8-44af-be9d-f8e98ba2071c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""47642ce2-40b2-40b0-bbe6-60b4a48daf16"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d5dcf4cb-f968-474b-a5aa-91de59928720"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78075a63-5869-4cd2-9e4f-0fa78aff7a58"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ca3e854-0210-4cb2-aeb0-2a93d4aab751"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AutoFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac770ecb-dd41-46ca-8bbb-5779a148cd1a"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fba89f44-c1c4-4e3c-9385-99adbfb6a4c7"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SemiFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""016f786c-f848-4947-9e52-67748e5b4a51"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ba53818-241a-44f8-8072-332d0311d69b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""827430da-6728-4750-b3a0-6aa2acc04930"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2acee60-5669-4b85-8291-a372b4d8fe66"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialPower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49983b03-a15b-4529-a43a-c48cae8c70f8"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4a7103a-31fb-4021-adb6-540d0f3c4ec9"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""5894fcc9-bccb-4835-b07d-3b09c1be7b25"",
            ""actions"": [
                {
                    ""name"": ""OpenCharacterMenu"",
                    ""type"": ""Button"",
                    ""id"": ""f3dd56b6-a9e1-40a2-9db1-e4428044d44c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5b5da29c-36ff-4b75-99f4-49bc8d000be1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCharacterMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Crouch = m_Gameplay.FindAction("Crouch", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_AutoFire = m_Gameplay.FindAction("AutoFire", throwIfNotFound: true);
        m_Gameplay_SemiFire = m_Gameplay.FindAction("SemiFire", throwIfNotFound: true);
        m_Gameplay_Aim = m_Gameplay.FindAction("Aim", throwIfNotFound: true);
        m_Gameplay_SwitchCharacter = m_Gameplay.FindAction("SwitchCharacter", throwIfNotFound: true);
        m_Gameplay_Spawn = m_Gameplay.FindAction("Spawn", throwIfNotFound: true);
        m_Gameplay_Reload = m_Gameplay.FindAction("Reload", throwIfNotFound: true);
        m_Gameplay_SpecialPower = m_Gameplay.FindAction("SpecialPower", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_OpenCharacterMenu = m_UI.FindAction("OpenCharacterMenu", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Crouch;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_AutoFire;
    private readonly InputAction m_Gameplay_SemiFire;
    private readonly InputAction m_Gameplay_Aim;
    private readonly InputAction m_Gameplay_SwitchCharacter;
    private readonly InputAction m_Gameplay_Spawn;
    private readonly InputAction m_Gameplay_Reload;
    private readonly InputAction m_Gameplay_SpecialPower;
    private readonly InputAction m_Gameplay_Pause;
    public struct GameplayActions
    {
        private @Controls m_Wrapper;
        public GameplayActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Crouch => m_Wrapper.m_Gameplay_Crouch;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @AutoFire => m_Wrapper.m_Gameplay_AutoFire;
        public InputAction @SemiFire => m_Wrapper.m_Gameplay_SemiFire;
        public InputAction @Aim => m_Wrapper.m_Gameplay_Aim;
        public InputAction @SwitchCharacter => m_Wrapper.m_Gameplay_SwitchCharacter;
        public InputAction @Spawn => m_Wrapper.m_Gameplay_Spawn;
        public InputAction @Reload => m_Wrapper.m_Gameplay_Reload;
        public InputAction @SpecialPower => m_Wrapper.m_Gameplay_SpecialPower;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Crouch.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @AutoFire.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAutoFire;
                @AutoFire.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAutoFire;
                @AutoFire.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAutoFire;
                @SemiFire.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSemiFire;
                @SemiFire.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSemiFire;
                @SemiFire.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSemiFire;
                @Aim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @SwitchCharacter.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchCharacter;
                @SwitchCharacter.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchCharacter;
                @SwitchCharacter.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchCharacter;
                @Spawn.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawn;
                @Spawn.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawn;
                @Spawn.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawn;
                @Reload.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReload;
                @SpecialPower.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialPower;
                @SpecialPower.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialPower;
                @SpecialPower.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialPower;
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @AutoFire.started += instance.OnAutoFire;
                @AutoFire.performed += instance.OnAutoFire;
                @AutoFire.canceled += instance.OnAutoFire;
                @SemiFire.started += instance.OnSemiFire;
                @SemiFire.performed += instance.OnSemiFire;
                @SemiFire.canceled += instance.OnSemiFire;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @SwitchCharacter.started += instance.OnSwitchCharacter;
                @SwitchCharacter.performed += instance.OnSwitchCharacter;
                @SwitchCharacter.canceled += instance.OnSwitchCharacter;
                @Spawn.started += instance.OnSpawn;
                @Spawn.performed += instance.OnSpawn;
                @Spawn.canceled += instance.OnSpawn;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @SpecialPower.started += instance.OnSpecialPower;
                @SpecialPower.performed += instance.OnSpecialPower;
                @SpecialPower.canceled += instance.OnSpecialPower;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_OpenCharacterMenu;
    public struct UIActions
    {
        private @Controls m_Wrapper;
        public UIActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenCharacterMenu => m_Wrapper.m_UI_OpenCharacterMenu;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @OpenCharacterMenu.started -= m_Wrapper.m_UIActionsCallbackInterface.OnOpenCharacterMenu;
                @OpenCharacterMenu.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnOpenCharacterMenu;
                @OpenCharacterMenu.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnOpenCharacterMenu;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OpenCharacterMenu.started += instance.OnOpenCharacterMenu;
                @OpenCharacterMenu.performed += instance.OnOpenCharacterMenu;
                @OpenCharacterMenu.canceled += instance.OnOpenCharacterMenu;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAutoFire(InputAction.CallbackContext context);
        void OnSemiFire(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnSwitchCharacter(InputAction.CallbackContext context);
        void OnSpawn(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnSpecialPower(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnOpenCharacterMenu(InputAction.CallbackContext context);
    }
}
