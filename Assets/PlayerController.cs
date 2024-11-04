using UnityEngine;
using KinematicCharacterController.Examples;

/// <summary>
/// Controls the player character.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public PlayerCharacterController Character;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        HandleCharacterInput();
    }

    private void HandleCharacterInput()
    {
        PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

        // Build the CharacterInputs struct
        // TODO: Replace with input manager scheme
        characterInputs.MoveAxisForward = InputManager.Instance.MoveInput.y;
        characterInputs.MoveAxisRight = InputManager.Instance.MoveInput.x;
        characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
        characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
        characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);

        // Apply inputs to character
        Character.SetInputs(ref characterInputs);
    }
}
