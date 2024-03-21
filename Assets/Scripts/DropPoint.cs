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
        public int Reward = 0;
        public Order(ItemDatabase itemsDB){
            List<Item> itemsAvaliables = new List<Item>();
            foreach(Item item in itemsDB.ItemTypes)
                itemsAvaliables.Add(item);
                
            int itemsInOrder = UnityEngine.Random.Range(1, 4);

            for(int i=0; i < itemsInOrder; i++){
                int itemAvaliable = UnityEngine.Random.Range(0, itemsAvaliables.Count);
                OrderInventory.Add(new InventoryEntry(){
                    ItemId = itemsAvaliables[itemAvaliable].Id,
                    Count = UnityEngine.Random.Range(3,21)
                });
                itemsAvaliables.Remove(itemsAvaliables[itemAvaliable]);
                Reward += (itemsDB.GetItem(OrderInventory[i].ItemId).SellingPrice * OrderInventory[i].Count);
            }
        }
    }
    
    public ItemDatabase m_itemDB;
    public static DropPoint Instance { get; private set; }
    public Order DropOrder;

    public bool isUsing = false;

    private void Awake(){
        Instance = this;
        m_itemDB.Init();
    }

    void Start(){
        GenerateNewOrder();
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

    public override int AddItem(string itemId, int amount){
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

    void GenerateNewOrder(){
        DropOrder = new Order(m_itemDB); 
    }
}
