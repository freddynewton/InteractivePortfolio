using System;
using System.Linq;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [Header("Interactable Manager Settings")]
    [SerializeField] private float _interactionRange = 3f;

    [Header("Tool Tip")]
    [SerializeField] private Transform _interactToolTip;
    [SerializeField] private Vector3 _toolTipOffset = new Vector3(0, 1, 0);

    private IInteractable currentInteractable;

    private void Update()
    {
        SetCurrentInteractable();
        SetToolTip();

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable?.Interact();
        }
    }

    private void SetToolTip()
    {
        if (currentInteractable == null)
        {
            _interactToolTip.gameObject.SetActive(false);
            return;
        }

        _interactToolTip.gameObject.SetActive(true);
        _interactToolTip.position = ((MonoBehaviour)currentInteractable).transform.position + _toolTipOffset;
    }

    private void SetCurrentInteractable()
    {
        IInteractable[] interactables = GetInteractables();

        if (interactables.Length == 0)
        {
            if (currentInteractable != null)
            {
                currentInteractable.Disable();
                currentInteractable = null;
            }
            return;
        }

        IInteractable closestInteractable = interactables.OrderBy(i => Vector3.Distance(transform.position, ((MonoBehaviour)i).transform.position)).First();

        if (currentInteractable != closestInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable.Disable();
                currentInteractable = null;
            }

            currentInteractable = closestInteractable;
        }
    }

    private IInteractable[] GetInteractables()
    {
        // Sphere Raycast
        Collider[] colliders = Physics.OverlapSphere(transform.position, _interactionRange);

        return System.Array.ConvertAll(colliders, c => c.GetComponent<IInteractable>()).Where(i => i != null).ToArray();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _interactionRange);
    }
}
