// GENERATED AUTOMATICALLY FROM 'Assets/CubeInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CubeInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CubeInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CubeInput"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""84346c3a-fabb-43a9-8b8f-07b7ec2572b7"",
            ""actions"": [
                {
                    ""name"": ""MovementForGamepad"",
                    ""type"": ""Button"",
                    ""id"": ""97aecc9a-046d-47fc-af43-0d2cb103f51f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectButtonOnGamepad"",
                    ""type"": ""Button"",
                    ""id"": ""699faa6c-883a-4c27-b408-a178297f6769"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9305ad9f-40e6-45c0-b402-6bd7c385315f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementForGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""b49d8ad8-d77d-4d77-8ed3-3acb851d4bdf"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementForGamepad"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6a014ec8-c4e1-4fb0-920a-19d5b0e54f54"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementForGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""abbb5bd3-6508-491b-8880-7cc6d1286f96"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementForGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4f4b2601-e8ba-4a63-ab16-ca89ce921d2d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementForGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5d97f1fd-f814-47ce-b720-59ffbd0866ec"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementForGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a1b56ca4-e3ac-4eeb-a146-659c6180701c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectButtonOnGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_MovementForGamepad = m_PlayerMovement.FindAction("MovementForGamepad", throwIfNotFound: true);
        m_PlayerMovement_SelectButtonOnGamepad = m_PlayerMovement.FindAction("SelectButtonOnGamepad", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_MovementForGamepad;
    private readonly InputAction m_PlayerMovement_SelectButtonOnGamepad;
    public struct PlayerMovementActions
    {
        private @CubeInput m_Wrapper;
        public PlayerMovementActions(@CubeInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementForGamepad => m_Wrapper.m_PlayerMovement_MovementForGamepad;
        public InputAction @SelectButtonOnGamepad => m_Wrapper.m_PlayerMovement_SelectButtonOnGamepad;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @MovementForGamepad.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovementForGamepad;
                @MovementForGamepad.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovementForGamepad;
                @MovementForGamepad.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovementForGamepad;
                @SelectButtonOnGamepad.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSelectButtonOnGamepad;
                @SelectButtonOnGamepad.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSelectButtonOnGamepad;
                @SelectButtonOnGamepad.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSelectButtonOnGamepad;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovementForGamepad.started += instance.OnMovementForGamepad;
                @MovementForGamepad.performed += instance.OnMovementForGamepad;
                @MovementForGamepad.canceled += instance.OnMovementForGamepad;
                @SelectButtonOnGamepad.started += instance.OnSelectButtonOnGamepad;
                @SelectButtonOnGamepad.performed += instance.OnSelectButtonOnGamepad;
                @SelectButtonOnGamepad.canceled += instance.OnSelectButtonOnGamepad;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovementForGamepad(InputAction.CallbackContext context);
        void OnSelectButtonOnGamepad(InputAction.CallbackContext context);
    }
}
