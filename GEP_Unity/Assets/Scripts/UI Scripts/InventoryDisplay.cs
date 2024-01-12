using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem; 
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);


    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot)
            {
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickUISlot)
    {
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;
        if (clickUISlot.AssignedInventorySlot.Data != null && mouseInventoryItem.AssignedInventorySlot.Data == null) 
        { 
            if(isShiftPressed && clickUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickUISlot.UpdateUISlot();
                return;
            }

            mouseInventoryItem.UpdateMouseSlot(clickUISlot.AssignedInventorySlot);
            clickUISlot.ClearSlot();
            return;
        }

        if (clickUISlot.AssignedInventorySlot.Data == null && mouseInventoryItem.AssignedInventorySlot.Data != null)
        {
            clickUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
            return;
        }

        if (clickUISlot.AssignedInventorySlot.Data != null && mouseInventoryItem.AssignedInventorySlot.Data != null)
        {
            bool isSameItem = clickUISlot.AssignedInventorySlot.Data == mouseInventoryItem.AssignedInventorySlot.Data;

            if (isSameItem && clickUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                clickUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                return;
            }
            else if(isSameItem && 
                !clickUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
            {
                if (leftInStack < 1)
                {
                    SwapSlots(clickUISlot);
                }
                else
                {
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.Data, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }
            else if (!isSameItem) 
            {
                SwapSlots(clickUISlot);
                return;
            }
        }
    }

    private void SwapSlots(InventorySlot_UI clickUISlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.Data, mouseInventoryItem.AssignedInventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();

        mouseInventoryItem.UpdateMouseSlot(clickUISlot.AssignedInventorySlot);

        clickUISlot.ClearSlot();
        clickUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickUISlot.UpdateUISlot();
    }
}
