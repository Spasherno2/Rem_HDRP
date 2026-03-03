using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactMask;

    [Header("Refs")]
    [SerializeField] private InteractPromptUI promptUI;
    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipmentController equipment;

    private IInteractable currentTarget;

    private void Awake()
    {
        if (inventory == null) inventory = GetComponent<Inventory>();
        if (equipment == null) equipment = GetComponent<EquipmentController>();
    }

    private void Update()
    {
        HandleHotbarInput();
        HandleRaycastAndInteract();
    }

    private void HandleHotbarInput()
    {
        if (equipment == null) return;

        if (TryGetHotbarIndex(out int index))
        {
            equipment.EquipSlot(index);
        }
    }

    private bool TryGetHotbarIndex(out int index)
    {
        index = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1)) { index = 0; return true; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { index = 1; return true; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { index = 2; return true; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { index = 3; return true; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { index = 4; return true; }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { index = 5; return true; }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { index = 6; return true; }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { index = 7; return true; }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { index = 8; return true; }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { index = 9; return true; } // slot 10

        return false;
    }

    private void HandleRaycastAndInteract()
    {
        currentTarget = null;

        if (playerCamera == null)
        {
            if (promptUI != null) promptUI.Hide();
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactMask, QueryTriggerInteraction.Ignore))
        {
            currentTarget = hit.collider.GetComponentInParent<IInteractable>();
        }

        if (currentTarget != null)
        {
            if (promptUI != null) promptUI.Show(currentTarget.GetPromptText());

            if (Input.GetKeyDown(currentTarget.InteractionKey))
            {
                currentTarget.Interact(this);
            }
        }
        else
        {
            if (promptUI != null) promptUI.Hide();
        }
    }

    // Expose inventory to interactables safely
    public Inventory Inventory => inventory;
    public EquipmentController Equipment => equipment;
}