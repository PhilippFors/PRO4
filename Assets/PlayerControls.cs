// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""0d591860-e460-46dd-a3e6-671aa64ca166"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""49c3cd4e-50fd-454b-971a-36715fbc91ce"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GrenadeAim"",
                    ""type"": ""Value"",
                    ""id"": ""299cfba4-0908-4f4f-b810-f00485ff6922"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2fdf1204-269a-4df8-a414-26bf038f12fb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftAttack"",
                    ""type"": ""Button"",
                    ""id"": ""a8dc81d1-93a0-4f15-96b9-e1f35f516585"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press,Hold(duration=0.4)""
                },
                {
                    ""name"": ""RightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""007d5ca4-4db2-4c16-8ceb-e0f0b6b8c58a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""8a44b34c-3cb8-46ab-9235-85e70d3d0002"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Button"",
                    ""id"": ""007e2556-65a8-473c-9722-4faded84be78"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill1"",
                    ""type"": ""Button"",
                    ""id"": ""304c72ee-f2ed-4ab1-8ec8-960f66b02e79"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Skill2"",
                    ""type"": ""Button"",
                    ""id"": ""497f7f01-7df4-4640-ab55-611cf8d07f16"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Skill3"",
                    ""type"": ""Button"",
                    ""id"": ""8d1161a3-e5ea-4443-8fd1-923edfa8bd0e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Skill4"",
                    ""type"": ""Button"",
                    ""id"": ""095ebc34-6d83-43a5-a007-20ad7e9144b1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""GrenadeThrow"",
                    ""type"": ""Button"",
                    ""id"": ""1b60ff2c-38aa-4ee8-be19-22d3faa49503"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""GrenadeReleaser"",
                    ""type"": ""Button"",
                    ""id"": ""954409b7-d197-48a4-8903-650524f31bc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""WeaponSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""860ffea1-ebda-4ca1-b6b5-59385a92a9df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""49eaa2ba-bd8a-4e61-adbc-b444370a892a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2b37d1ad-fe27-4fc8-aa29-ef58443a89c7"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a98c3121-dcb6-45cc-b95b-7679dfa6da22"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a023282-d76d-4030-9476-7c3d5ccdfa03"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""LeftAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""666a5262-b0cf-4bc9-88a8-8d38b7a474ec"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""wasdKeys"",
                    ""id"": ""c14f9cd8-913c-40b0-ac09-e9a85fb7bed6"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""973f8379-f909-4521-92a3-2091804577fe"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""3e92b97b-83f2-4194-b46b-fc4cbcffbbc9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""62b25473-ed11-4b80-9660-7f04cf19d5f3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""50244d5c-9c2a-4cdf-845c-3249bbfde9a5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""55565c4e-2f1a-4342-99e2-b40932efd8f8"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc645fe7-e4ea-432c-a5d1-488cc3a7fcd8"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d9a4918-cb42-4709-86a3-56157f5082f4"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""RightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c395539-0c7e-453e-b1e9-f639500d73c8"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""939b7dc7-8d6f-4b2b-b364-fe415066da5d"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5145e25a-5f34-47fd-b402-01e3312dfa64"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e70b0e6-bcfe-4e23-badd-96fb6e04c094"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b297393a-c648-438e-bead-fc1ec0396624"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5352cccf-3c6f-46a5-bdb5-8db5f049686c"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ab38ee3-cb62-422a-a6d1-7c76c0e973b9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8d2a8eb-b555-47b1-9d91-658b3b6a584a"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Skill4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0643772d-9463-4f87-8f76-5ca42b040505"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skill4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec945e99-fe3c-492a-a39a-3ae26524441b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5d1a22f-c89a-4677-a25f-7fff3748a9c3"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GrenadeThrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bcf75fd9-bead-4098-aa10-d1c1739cad9c"",
                    ""path"": ""<Keyboard>/G"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""GrenadeThrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa3632cd-0d28-46dd-8875-cc7a9cebe382"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GrenadeAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""wasdKeys"",
                    ""id"": ""652bb1c8-bb58-4da7-8c56-b0c0a0195fac"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GrenadeAim"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""cfff9400-3e4d-4e62-87a6-a0abb7c04e14"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""GrenadeAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""f6124f69-7c53-44f8-88ff-1d55c900ca34"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""GrenadeAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""bdf49beb-c6bf-469c-9211-9139845d8133"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""GrenadeAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""40e8585e-27cd-412c-b69d-0b0f7fc4178f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""GrenadeAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""76c72d61-b60d-45e1-80ca-d1e6bd861266"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GrenadeReleaser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""189b8616-b4eb-4792-b219-78ed713bffd5"",
                    ""path"": ""<Keyboard>/G"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""GrenadeReleaser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb8d1d9e-4a53-4b65-b1f9-d20fa0eb97d6"",
                    ""path"": ""<Keyboard>/Z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""WeaponSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""712d7f10-7adc-45f0-9759-181d44eb844b"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""WeaponSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50c91935-d699-4707-9508-06527fc3394d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d0e7bea-9163-4fb4-9034-bb2097cc33d1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""uiControls"",
            ""id"": ""e18a87e5-b7cf-472e-bb52-12a680308cf5"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""b43fa7b8-1c3e-47b8-8f21-e7556d612f03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cf71d3a9-9491-4cc5-9304-2460ac08e5b6"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cedcc12e-a8f5-43fe-8c63-e57d3fd6a76d"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard and mouse"",
            ""bindingGroup"": ""Keyboard and mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_GrenadeAim = m_Gameplay.FindAction("GrenadeAim", throwIfNotFound: true);
        m_Gameplay_Rotate = m_Gameplay.FindAction("Rotate", throwIfNotFound: true);
        m_Gameplay_LeftAttack = m_Gameplay.FindAction("LeftAttack", throwIfNotFound: true);
        m_Gameplay_RightAttack = m_Gameplay.FindAction("RightAttack", throwIfNotFound: true);
        m_Gameplay_Dash = m_Gameplay.FindAction("Dash", throwIfNotFound: true);
        m_Gameplay_Look = m_Gameplay.FindAction("Look", throwIfNotFound: true);
        m_Gameplay_Skill1 = m_Gameplay.FindAction("Skill1", throwIfNotFound: true);
        m_Gameplay_Skill2 = m_Gameplay.FindAction("Skill2", throwIfNotFound: true);
        m_Gameplay_Skill3 = m_Gameplay.FindAction("Skill3", throwIfNotFound: true);
        m_Gameplay_Skill4 = m_Gameplay.FindAction("Skill4", throwIfNotFound: true);
        m_Gameplay_GrenadeThrow = m_Gameplay.FindAction("GrenadeThrow", throwIfNotFound: true);
        m_Gameplay_GrenadeReleaser = m_Gameplay.FindAction("GrenadeReleaser", throwIfNotFound: true);
        m_Gameplay_WeaponSwitch = m_Gameplay.FindAction("WeaponSwitch", throwIfNotFound: true);
        m_Gameplay_Interact = m_Gameplay.FindAction("Interact", throwIfNotFound: true);
        // uiControls
        m_uiControls = asset.FindActionMap("uiControls", throwIfNotFound: true);
        m_uiControls_Pause = m_uiControls.FindAction("Pause", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_GrenadeAim;
    private readonly InputAction m_Gameplay_Rotate;
    private readonly InputAction m_Gameplay_LeftAttack;
    private readonly InputAction m_Gameplay_RightAttack;
    private readonly InputAction m_Gameplay_Dash;
    private readonly InputAction m_Gameplay_Look;
    private readonly InputAction m_Gameplay_Skill1;
    private readonly InputAction m_Gameplay_Skill2;
    private readonly InputAction m_Gameplay_Skill3;
    private readonly InputAction m_Gameplay_Skill4;
    private readonly InputAction m_Gameplay_GrenadeThrow;
    private readonly InputAction m_Gameplay_GrenadeReleaser;
    private readonly InputAction m_Gameplay_WeaponSwitch;
    private readonly InputAction m_Gameplay_Interact;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @GrenadeAim => m_Wrapper.m_Gameplay_GrenadeAim;
        public InputAction @Rotate => m_Wrapper.m_Gameplay_Rotate;
        public InputAction @LeftAttack => m_Wrapper.m_Gameplay_LeftAttack;
        public InputAction @RightAttack => m_Wrapper.m_Gameplay_RightAttack;
        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
        public InputAction @Look => m_Wrapper.m_Gameplay_Look;
        public InputAction @Skill1 => m_Wrapper.m_Gameplay_Skill1;
        public InputAction @Skill2 => m_Wrapper.m_Gameplay_Skill2;
        public InputAction @Skill3 => m_Wrapper.m_Gameplay_Skill3;
        public InputAction @Skill4 => m_Wrapper.m_Gameplay_Skill4;
        public InputAction @GrenadeThrow => m_Wrapper.m_Gameplay_GrenadeThrow;
        public InputAction @GrenadeReleaser => m_Wrapper.m_Gameplay_GrenadeReleaser;
        public InputAction @WeaponSwitch => m_Wrapper.m_Gameplay_WeaponSwitch;
        public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @GrenadeAim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrenadeAim;
                @GrenadeAim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrenadeAim;
                @GrenadeAim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrenadeAim;
                @Rotate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @LeftAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftAttack;
                @LeftAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftAttack;
                @LeftAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftAttack;
                @RightAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightAttack;
                @RightAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightAttack;
                @RightAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightAttack;
                @Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Look.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Skill1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill1;
                @Skill1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill1;
                @Skill1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill1;
                @Skill2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill2;
                @Skill2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill2;
                @Skill2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill2;
                @Skill3.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill3;
                @Skill3.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill3;
                @Skill3.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill3;
                @Skill4.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill4;
                @Skill4.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill4;
                @Skill4.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill4;
                @GrenadeThrow.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrenadeThrow;
                @GrenadeThrow.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrenadeThrow;
                @GrenadeThrow.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrenadeThrow;
                @GrenadeReleaser.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrenadeReleaser;
                @GrenadeReleaser.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrenadeReleaser;
                @GrenadeReleaser.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrenadeReleaser;
                @WeaponSwitch.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnWeaponSwitch;
                @WeaponSwitch.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnWeaponSwitch;
                @WeaponSwitch.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnWeaponSwitch;
                @Interact.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @GrenadeAim.started += instance.OnGrenadeAim;
                @GrenadeAim.performed += instance.OnGrenadeAim;
                @GrenadeAim.canceled += instance.OnGrenadeAim;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @LeftAttack.started += instance.OnLeftAttack;
                @LeftAttack.performed += instance.OnLeftAttack;
                @LeftAttack.canceled += instance.OnLeftAttack;
                @RightAttack.started += instance.OnRightAttack;
                @RightAttack.performed += instance.OnRightAttack;
                @RightAttack.canceled += instance.OnRightAttack;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Skill1.started += instance.OnSkill1;
                @Skill1.performed += instance.OnSkill1;
                @Skill1.canceled += instance.OnSkill1;
                @Skill2.started += instance.OnSkill2;
                @Skill2.performed += instance.OnSkill2;
                @Skill2.canceled += instance.OnSkill2;
                @Skill3.started += instance.OnSkill3;
                @Skill3.performed += instance.OnSkill3;
                @Skill3.canceled += instance.OnSkill3;
                @Skill4.started += instance.OnSkill4;
                @Skill4.performed += instance.OnSkill4;
                @Skill4.canceled += instance.OnSkill4;
                @GrenadeThrow.started += instance.OnGrenadeThrow;
                @GrenadeThrow.performed += instance.OnGrenadeThrow;
                @GrenadeThrow.canceled += instance.OnGrenadeThrow;
                @GrenadeReleaser.started += instance.OnGrenadeReleaser;
                @GrenadeReleaser.performed += instance.OnGrenadeReleaser;
                @GrenadeReleaser.canceled += instance.OnGrenadeReleaser;
                @WeaponSwitch.started += instance.OnWeaponSwitch;
                @WeaponSwitch.performed += instance.OnWeaponSwitch;
                @WeaponSwitch.canceled += instance.OnWeaponSwitch;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // uiControls
    private readonly InputActionMap m_uiControls;
    private IUiControlsActions m_UiControlsActionsCallbackInterface;
    private readonly InputAction m_uiControls_Pause;
    public struct UiControlsActions
    {
        private @PlayerControls m_Wrapper;
        public UiControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_uiControls_Pause;
        public InputActionMap Get() { return m_Wrapper.m_uiControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UiControlsActions set) { return set.Get(); }
        public void SetCallbacks(IUiControlsActions instance)
        {
            if (m_Wrapper.m_UiControlsActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_UiControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UiControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UiControlsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_UiControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public UiControlsActions @uiControls => new UiControlsActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardandmouseSchemeIndex = -1;
    public InputControlScheme KeyboardandmouseScheme
    {
        get
        {
            if (m_KeyboardandmouseSchemeIndex == -1) m_KeyboardandmouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and mouse");
            return asset.controlSchemes[m_KeyboardandmouseSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnGrenadeAim(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnLeftAttack(InputAction.CallbackContext context);
        void OnRightAttack(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSkill1(InputAction.CallbackContext context);
        void OnSkill2(InputAction.CallbackContext context);
        void OnSkill3(InputAction.CallbackContext context);
        void OnSkill4(InputAction.CallbackContext context);
        void OnGrenadeThrow(InputAction.CallbackContext context);
        void OnGrenadeReleaser(InputAction.CallbackContext context);
        void OnWeaponSwitch(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IUiControlsActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}
