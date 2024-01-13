using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete {  get; set; }

    public void Interact(Interactor interactor, out bool interactionSuccessful)
    {
        OnDynamicsInventoryDisplayRequested?.Invoke(primaryInventorySystem);
        interactionSuccessful = true;
    }

    public void EndInteraction()
    {

    }
}
