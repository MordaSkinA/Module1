using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IPossessable
{
    [SerializeField] private float rotationSpeed = 12f;
    [SerializeField] private float runMultiplier = 3f;
    [SerializeField] private Transform cameraTransform;

    private Module1 playerInput;
    private CharacterController characterController;

    private Vector2 rawInput;
    private Vector3 currentMovement;
    private Vector3 currentRunMovement;
    private bool isMovementPressed;
    private bool isRunPressed;

    private void Awake()
    {
        playerInput = new Module1();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRunInput;
        playerInput.CharacterControls.Run.canceled += OnRunInput;
    }

    public void OnPossess()
    {
        playerInput.CharacterControls.Enable();
    }

    public void OnUnpossess()
    {
        playerInput.CharacterControls.Disable();
    }

    private void Update()
    {
        UpdateMovementDirection();
        RotateTowardsMovementDirection();
        Move();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        rawInput = context.ReadValue<Vector2>();
        isMovementPressed = rawInput.sqrMagnitude > 0f;
    }

    private void OnRunInput(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    private void UpdateMovementDirection()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 desiredDirection = camForward * rawInput.y + camRight * rawInput.x;
        currentMovement = Vector3.ClampMagnitude(desiredDirection, 1f);
        currentRunMovement = currentMovement * runMultiplier;
    }

    private void Move()
    {
        Vector3 movement = isRunPressed ? currentRunMovement : currentMovement;
        characterController.Move(movement * Time.deltaTime);
    }

    private void RotateTowardsMovementDirection()
    {
        if (!isMovementPressed)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(currentMovement);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}