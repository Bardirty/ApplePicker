using TMPro;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private TextMeshProUGUI interactionText;
    private bool isPressed = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        InteractionRay();
    }
    private void InteractionRay()
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            NPCInteraction interactable = hit.collider.GetComponent<NPCInteraction>();
            if (interactable != null)
            {
                hitSomething = !isPressed;
                interactionText.text = interactable.GetDesccription();
                if (Input.GetKeyDown(KeyCode.E) && !isPressed)
                {
                    interactable.Interact();
                    isPressed = true;
                }
            }
        }
        else isPressed = false;

        interactionUI.SetActive(hitSomething);
    }
}

