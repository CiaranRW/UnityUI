using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay playerBackpackPanel;
    public GameObject inventoryUI;

    private void Awake()
    {
        chestPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicsInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicsInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
    }

    private void Update()
    {
        if (chestPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) 
        {
            chestPanel.gameObject.SetActive(false);
        }
        if (playerBackpackPanel.gameObject.activeInHierarchy &&
            (Keyboard.current.escapeKey.wasPressedThisFrame))
        {
            playerBackpackPanel.gameObject.SetActive(false);
        }
        if (inventoryUI.gameObject.activeInHierarchy &&
           (Keyboard.current.escapeKey.wasPressedThisFrame))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inventoryUI.gameObject.SetActive(false);

        }
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);
    }

    void DisplayPlayerBackpack(InventorySystem invToDisplay)
    {
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(invToDisplay);
    }
}
