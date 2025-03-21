//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/CodeBase/Input/InputMap.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace MorningThriller.InputManagement
{
    public partial class @InputMap: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputMap()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""821be78a-b1b0-4d56-b578-8c876193d34a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""380d3939-2da9-47fe-a343-d60244e78f3c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""70dc7ab8-53cd-44f7-8abb-bcb604873ff3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ItemUsed"",
                    ""type"": ""Button"",
                    ""id"": ""62eb7ed2-7bb8-4ff1-8666-d3e585861385"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ItemScroll"",
                    ""type"": ""Value"",
                    ""id"": ""87adf2f2-cac9-4af2-b81a-bd4f8f090f9b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ItemSwitchToFirst"",
                    ""type"": ""Button"",
                    ""id"": ""dae271dc-aa2e-4ee7-8653-e5249525d87f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ItemSwitchToSecond"",
                    ""type"": ""Button"",
                    ""id"": ""625ea54a-14b1-426a-b7d7-d5a7984798e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ItemSwitchToThird"",
                    ""type"": ""Button"",
                    ""id"": ""c939f0e2-7e61-48f7-beed-6d98b63edc62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ItemSwitchToFourth"",
                    ""type"": ""Button"",
                    ""id"": ""4800f1e7-3c0b-4673-ac66-d7dd29acc9da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""fbc3bcc3-2219-4544-b055-5bb5a84dbbc5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""862a01d0-30fe-492b-8ccb-97b3ddbff92a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e56a7a3c-021d-414f-a094-2133ba9e1c03"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a9f47c4a-c6e2-4f76-9ba4-554aba8e514f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c7759f33-19fd-4656-82a3-e18a7a42e8aa"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4f549159-f7f0-4dcc-ba0d-d849501c4b73"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""ItemUsed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""290a5e0d-2876-4650-87d8-91bdad9ce8da"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ItemScroll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e207be3a-6e6a-4912-85c2-e7c83db5d1eb"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""ItemScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""356a0323-0fe9-43a2-98b5-3202d4d3ace9"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""ItemScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fc77d1ca-614d-47dc-9fe3-4b75436d9997"",
                    ""path"": ""<Mouse>/scroll/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""ItemScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""24179a38-b1f0-4af3-a223-24de2d74f1f0"",
                    ""path"": ""<Mouse>/scroll/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""ItemScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a6002203-5747-41e8-bd92-f3f77fb36782"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""ItemSwitchToFirst"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0514f012-cf4e-4ad8-81df-8862474179ed"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""ItemSwitchToSecond"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e32e4674-5fb6-4e2d-be20-1a82a5ef274e"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""ItemSwitchToThird"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad6029cb-bd49-4552-9d2e-440083c90d33"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Base"",
                    ""action"": ""ItemSwitchToFourth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd9d8cb3-61cc-4076-9d89-b095379d89db"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""695aa7bc-fc79-4cd5-914d-1881d1ab12af"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Base"",
            ""bindingGroup"": ""Base"",
            ""devices"": []
        }
    ]
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
            m_Player_Roll = m_Player.FindAction("Roll", throwIfNotFound: true);
            m_Player_ItemUsed = m_Player.FindAction("ItemUsed", throwIfNotFound: true);
            m_Player_ItemScroll = m_Player.FindAction("ItemScroll", throwIfNotFound: true);
            m_Player_ItemSwitchToFirst = m_Player.FindAction("ItemSwitchToFirst", throwIfNotFound: true);
            m_Player_ItemSwitchToSecond = m_Player.FindAction("ItemSwitchToSecond", throwIfNotFound: true);
            m_Player_ItemSwitchToThird = m_Player.FindAction("ItemSwitchToThird", throwIfNotFound: true);
            m_Player_ItemSwitchToFourth = m_Player.FindAction("ItemSwitchToFourth", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
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

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Player
        private readonly InputActionMap m_Player;
        private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
        private readonly InputAction m_Player_Move;
        private readonly InputAction m_Player_Roll;
        private readonly InputAction m_Player_ItemUsed;
        private readonly InputAction m_Player_ItemScroll;
        private readonly InputAction m_Player_ItemSwitchToFirst;
        private readonly InputAction m_Player_ItemSwitchToSecond;
        private readonly InputAction m_Player_ItemSwitchToThird;
        private readonly InputAction m_Player_ItemSwitchToFourth;
        public struct PlayerActions
        {
            private @InputMap m_Wrapper;
            public PlayerActions(@InputMap wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @Roll => m_Wrapper.m_Player_Roll;
            public InputAction @ItemUsed => m_Wrapper.m_Player_ItemUsed;
            public InputAction @ItemScroll => m_Wrapper.m_Player_ItemScroll;
            public InputAction @ItemSwitchToFirst => m_Wrapper.m_Player_ItemSwitchToFirst;
            public InputAction @ItemSwitchToSecond => m_Wrapper.m_Player_ItemSwitchToSecond;
            public InputAction @ItemSwitchToThird => m_Wrapper.m_Player_ItemSwitchToThird;
            public InputAction @ItemSwitchToFourth => m_Wrapper.m_Player_ItemSwitchToFourth;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void AddCallbacks(IPlayerActions instance)
            {
                if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @ItemUsed.started += instance.OnItemUsed;
                @ItemUsed.performed += instance.OnItemUsed;
                @ItemUsed.canceled += instance.OnItemUsed;
                @ItemScroll.started += instance.OnItemScroll;
                @ItemScroll.performed += instance.OnItemScroll;
                @ItemScroll.canceled += instance.OnItemScroll;
                @ItemSwitchToFirst.started += instance.OnItemSwitchToFirst;
                @ItemSwitchToFirst.performed += instance.OnItemSwitchToFirst;
                @ItemSwitchToFirst.canceled += instance.OnItemSwitchToFirst;
                @ItemSwitchToSecond.started += instance.OnItemSwitchToSecond;
                @ItemSwitchToSecond.performed += instance.OnItemSwitchToSecond;
                @ItemSwitchToSecond.canceled += instance.OnItemSwitchToSecond;
                @ItemSwitchToThird.started += instance.OnItemSwitchToThird;
                @ItemSwitchToThird.performed += instance.OnItemSwitchToThird;
                @ItemSwitchToThird.canceled += instance.OnItemSwitchToThird;
                @ItemSwitchToFourth.started += instance.OnItemSwitchToFourth;
                @ItemSwitchToFourth.performed += instance.OnItemSwitchToFourth;
                @ItemSwitchToFourth.canceled += instance.OnItemSwitchToFourth;
            }

            private void UnregisterCallbacks(IPlayerActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
                @Roll.started -= instance.OnRoll;
                @Roll.performed -= instance.OnRoll;
                @Roll.canceled -= instance.OnRoll;
                @ItemUsed.started -= instance.OnItemUsed;
                @ItemUsed.performed -= instance.OnItemUsed;
                @ItemUsed.canceled -= instance.OnItemUsed;
                @ItemScroll.started -= instance.OnItemScroll;
                @ItemScroll.performed -= instance.OnItemScroll;
                @ItemScroll.canceled -= instance.OnItemScroll;
                @ItemSwitchToFirst.started -= instance.OnItemSwitchToFirst;
                @ItemSwitchToFirst.performed -= instance.OnItemSwitchToFirst;
                @ItemSwitchToFirst.canceled -= instance.OnItemSwitchToFirst;
                @ItemSwitchToSecond.started -= instance.OnItemSwitchToSecond;
                @ItemSwitchToSecond.performed -= instance.OnItemSwitchToSecond;
                @ItemSwitchToSecond.canceled -= instance.OnItemSwitchToSecond;
                @ItemSwitchToThird.started -= instance.OnItemSwitchToThird;
                @ItemSwitchToThird.performed -= instance.OnItemSwitchToThird;
                @ItemSwitchToThird.canceled -= instance.OnItemSwitchToThird;
                @ItemSwitchToFourth.started -= instance.OnItemSwitchToFourth;
                @ItemSwitchToFourth.performed -= instance.OnItemSwitchToFourth;
                @ItemSwitchToFourth.canceled -= instance.OnItemSwitchToFourth;
            }

            public void RemoveCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPlayerActions instance)
            {
                foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
        public struct UIActions
        {
            private @InputMap m_Wrapper;
            public UIActions(@InputMap wrapper) { m_Wrapper = wrapper; }
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void AddCallbacks(IUIActions instance)
            {
                if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            }

            private void UnregisterCallbacks(IUIActions instance)
            {
            }

            public void RemoveCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IUIActions instance)
            {
                foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public UIActions @UI => new UIActions(this);
        private int m_BaseSchemeIndex = -1;
        public InputControlScheme BaseScheme
        {
            get
            {
                if (m_BaseSchemeIndex == -1) m_BaseSchemeIndex = asset.FindControlSchemeIndex("Base");
                return asset.controlSchemes[m_BaseSchemeIndex];
            }
        }
        public interface IPlayerActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnRoll(InputAction.CallbackContext context);
            void OnItemUsed(InputAction.CallbackContext context);
            void OnItemScroll(InputAction.CallbackContext context);
            void OnItemSwitchToFirst(InputAction.CallbackContext context);
            void OnItemSwitchToSecond(InputAction.CallbackContext context);
            void OnItemSwitchToThird(InputAction.CallbackContext context);
            void OnItemSwitchToFourth(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
        }
    }
}
