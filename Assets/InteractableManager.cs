using System.Linq;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [Header("Interactable Manager Settings")]
    [SerializeField] private float InteractionRange = 5f;

    private IInteractable currentInteractable;

    private void Update()
    {
        SetCurrentInteractable();

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
        }
    }

    private void SetCurrentInteractable()
    {
        IInteractable[] interactables = GetInteractables();

        if (interactables.Length == 0)
        {
            if (currentInteractable != null)
            {
                currentInteractable = null;
            }
            return;
        }

        IInteractable closestInteractable = interactables.OrderBy(i => Vector3.Distance(transform.position, ((MonoBehaviour)i).transform.position)).First();

        if (currentInteractable != closestInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable = null;
            }

            currentInteractable = closestInteractable;
        }
    }

    private IInteractable[] GetInteractables()
    {
        // Sphere Raycast
        Collider[] colliders = Physics.OverlapSphere(transform.position, InteractionRange);

        return System.Array.ConvertAll(colliders, c => c.GetComponent<IInteractable>()).Where(i => i != null).ToArray();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, InteractionRange);
    }
}
