using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryEntry{
        public string ItemId;
        public int Count;
    }

    [Tooltip("-1 is infinite")]
    public int InventorySpace = -1;
    protected List<InventoryEntry> m_Inventory = new List<InventoryEntry>();
    public List<InventoryEntry> InventoryEntryList => m_Inventory;
    [SerializeField] protected int m_CurrentAmount = 0;

    public int AddItem(string itemId, int amount)
    {
        //as we use the shortcut -1 = infinite amount, we need to actually set it to max value for computation following
        int maxInventorySpace = InventorySpace == -1 ? Int32.MaxValue : InventorySpace;
        
        if (m_CurrentAmount == maxInventorySpace)
            return amount;

        int found = m_Inventory.FindIndex(entry => entry.ItemId == itemId);
        int addedAmount = Mathf.Min(maxInventorySpace - m_CurrentAmount, amount);
        
        //couldn't find an entry for that resource id so we add a new one.
        if (found == -1)
        {
            m_Inventory.Add(new InventoryEntry()
            {
                ItemId = itemId,
                Count = addedAmount
            });
        }
        else
        {
            m_Inventory[found].Count += addedAmount;
        }

        m_CurrentAmount += addedAmount;
        return amount - addedAmount;
    }

    //return how much was actually removed, will be 0 if couldn't get any.
    public int GetItem(string resourceId, int requestAmount)
    {
        int found = m_Inventory.FindIndex(entry => entry.ItemId == resourceId);
        
        //couldn't find an entry for that resource id so we add a new one.
        if (found != -1)
        {
            int amount = Mathf.Min(requestAmount, m_Inventory[found].Count);
            m_Inventory[found].Count -= amount;

            if (m_Inventory[found].Count == 0)
            {//no more of that resources, so we remove it
                m_Inventory.RemoveAt(found);
            }

            m_CurrentAmount -= amount;

            return amount;
        }

        return 0;
    }
}
