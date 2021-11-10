// GENERATED AUTOMATICALLY FROM 'Assets/Samples/Keyboard Interaction Tester/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""218305ae-9446-4dd2-afec-342c4aa03f89"",
            ""actions"": [
                {
                    ""name"": ""Activate"",
                    ""type"": ""Button"",
                    ""id"": ""e89f062b-3b0e-4bd3-b592-c2c8508e8453"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Deactivate"",
                    ""type"": ""Button"",
                    ""id"": ""50b9dc6b-ce1c-44eb-9b12-c659eb96559b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""be80ed56-e3da-4ad3-baca-4e7772dc2077"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Activate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46de246e-8bba-480d-9acd-66bf644ad588"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Deactivate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Activate = m_Player.FindAction("Activate", throwIfNotFound: true);
        m_Player_Deactivate = m_Player.FindAction("Deactivate", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Activate;
    private readonly InputAction m_Player_Deactivate;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Activate => m_Wrapper.m_Player_Activate;
        public InputAction @Deactivate => m_Wrapper.m_Player_Deactivate;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Activate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActivate;
                @Activate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActivate;
                @Activate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActivate;
                @Deactivate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeactivate;
                @Deactivate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeactivate;
                @Deactivate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeactivate;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Activate.started += instance.OnActivate;
                @Activate.performed += instance.OnActivate;
                @Activate.canceled += instance.OnActivate;
                @Deactivate.started += instance.OnDeactivate;
                @Deactivate.performed += instance.OnDeactivate;
                @Deactivate.canceled += instance.OnDeactivate;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnActivate(InputAction.CallbackContext context);
        void OnDeactivate(InputAction.CallbackContext context);
    }
}
