using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public GameObject backpack;

    private bool test;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    protected override void Awake()
    {
        base.Awake();

        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
    }


    void Update()
    {
        if(Keyboard.current.iKey.wasPressedThisFrame)
        {
            if(backpack.activeSelf == true)
            {
                OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);
                test = true;
            }
        }
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if (primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        else if (secondaryInventorySystem.AddToInventory(data,amount))
        { 
            return true;
        }

        return false;
    }


}
