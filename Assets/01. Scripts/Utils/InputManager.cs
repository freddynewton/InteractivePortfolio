using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages player input using the new Input System
/// </summary>
public class InputManager : Singleton<InputManager>
{
    // Public properties to access the player input
    [HideInInspector] public Vector2 MoveInput;
    [HideInInspector] public Vector2 LookInput;
    
    // Serialized field to reference the InputActionAsset
    [SerializeField] private InputActionAsset _inputActions;

    // Private fields to store the player input actions
    private InputActionMap _playerInputActionMap;
    private InputAction _moveAction;
    private InputAction _lookAction;

    private bool _isInitialized;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    public override void Awake()
    {
        base.Awake();

        _initialize();
    }

    /// <summary>
    /// Initializes the Input Manager by setting up the player input actions
    /// </summary>
    private void _initialize()
    {
        // Null check to prevent re-initialization
        if (_inputActions == null)
        {
            Debug.LogError("Input Actions not set in the Input Manager component on " + gameObject.name, this);
            return;
        }

        // Null check to prevent re-initialization
        if (_isInitialized)
        {
            return;
        }

        // Find the player input action map and the move and look actions
        _playerInputActionMap = _inputActions.FindActionMap("Player");
        _moveAction = _playerInputActionMap.FindAction("Move");
        _lookAction = _playerInputActionMap.FindAction("Look");

        _moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _moveAction.canceled += ctx => MoveInput = Vector2.zero;
        
        _lookAction.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        _lookAction.canceled += ctx => LookInput = Vector2.zero;

        // Enable the player input actions
        _inputActions.Enable();

        _isInitialized = true;
    }
}
