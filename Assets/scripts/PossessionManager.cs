using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public interface IPossessable
{
    void OnPossess();
    void OnUnpossess();
}

public class PossessionManager : MonoBehaviour
{
    [SerializeField] private float possessionRadius = 3f;
    [SerializeField] private LayerMask possessableLayer;
    [SerializeField] private MonoBehaviour startingBody;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private Module1 playerInput;
    private MonoBehaviour currentBody;

    private void Awake()
    {
        playerInput = new Module1();
        playerInput.CharacterControls.Possess.performed += OnPossessInput;
    }

    private void Start()
    {
        if (startingBody is IPossessable possessable)
        {
            Possess(startingBody, possessable);
        }
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }

    private void OnPossessInput(InputAction.CallbackContext context)
    {
        TryPossessNearbyBody();
    }

    private void TryPossessNearbyBody()
    {
        Vector3 originPosition = currentBody != null ? currentBody.transform.position : transform.position;
        Collider[] hits = Physics.OverlapSphere(originPosition, possessionRadius, possessableLayer);

        foreach (Collider hit in hits)
        {
            IPossessable possessable = hit.GetComponent<IPossessable>();
            MonoBehaviour target = possessable as MonoBehaviour;

            if (possessable != null && target != currentBody)
            {
                Possess(target, possessable);
                return;
            }
        }
    }

    private void Possess(MonoBehaviour newBody, IPossessable possessable)
    {
        if (currentBody != null && currentBody is IPossessable oldPossessable)
        {
            oldPossessable.OnUnpossess();
            currentBody.enabled = false;
        }

        newBody.enabled = true;
        possessable.OnPossess();

        currentBody = newBody;

        cinemachineCamera.Follow = newBody.transform;
        cinemachineCamera.LookAt = newBody.transform;
    }
}