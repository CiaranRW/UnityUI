using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;

    private void OnEnable()
    {
        InventoryHolder.OnDynamicsInventoryDisplayRequested += DisplayInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicsInventoryDisplayRequested -= DisplayInventory;
    }

    private void Update()
    {
        
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay);
    }
}
