using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A special building that hold a static reference so it can be found by other script easily (e.g. for Unit to go back
/// to it)
/// </summary>
public class DropPoint : Inventory
{ 
    [System.Serializable]
    public class Order{
        public List<InventoryEntry> OrderInventory = new List<InventoryEntry>();
    }

    public static DropPoint Instance { get; private set; }
    public Order DropOrder;

    public bool isUsing = false;

    private void Awake()
    {
        Instance = this;
        SetInventoryByOrder();
    }

    void SetInventoryByOrder(){
        InventorySpace = 0;
        foreach(InventoryEntry orderEntry in DropOrder.OrderInventory){
            m_Inventory.Add(new InventoryEntry()
            {
                ItemId = orderEntry.ItemId, 
                Count = 0
            });
            InventorySpace += orderEntry.Count;
        }
    }

    public override int AddItem(string itemId, int amount)
    {
        int foundInOrder = DropOrder.OrderInventory.FindIndex(entry => entry.ItemId == itemId);
        int foundInInventory = DropOrder.OrderInventory.FindIndex(entry => entry.ItemId == itemId);
        
        try{
            int addedAmount = Mathf.Min(DropOrder.OrderInventory[foundInOrder].Count - m_Inventory[foundInInventory].Count, amount);
            m_Inventory[foundInInventory].Count += addedAmount;
        
            if (addedAmount == 0)
                return amount;
            
            m_CurrentAmount += addedAmount;
            return amount - addedAmount;

        }catch(Exception e){
            Debug.LogWarning($"This item isn't be in the order: {e}");
            return 0;
        }
    }
}
