using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A special building that hold a static reference so it can be found by other script easily (e.g. for Unit to go back
/// to it)
/// </summary>
public class DropPoint : MonoBehaviour, IStorageInteractable
{   
    public static DropPoint Instance { get; private set; }
    public Order DropOrder;
    private int m_CurrentTotalStock = 0;

    public bool isUsing = false;

    private OrderPresenter m_OrderPresenter;

    private void Awake(){
        Instance = this;
        m_OrderPresenter = new OrderPresenter(this, FindObjectOfType<OrderView>());
    }

    void Start(){
        GenerateNewOrder();
    }

    public int AddItem(string itemId, int amount){
        int found = DropOrder.OrderInventory.FindIndex(entry => entry.ItemId == itemId);
        
        try{
            int addedAmount = Mathf.Min(DropOrder.OrderInventory[found].OrderedStock - DropOrder.OrderInventory[found].CurrentStock, amount);
            DropOrder.OrderInventory[found].CurrentStock += addedAmount;
        
            if (addedAmount == 0)
                return amount;
            
            m_CurrentTotalStock += addedAmount;

            m_OrderPresenter.OnInventoryChanged(itemId);
            if(DropOrder.IsOrderCompleted())
                GenerateNewOrder();

            return amount - addedAmount;

        }catch(Exception e){
            Debug.LogWarning($"This item isn't be in the order list: {e}");
            return amount;
        }
    }

    void GenerateNewOrder(){
        DropOrder = new Order(WarehouseStorage.Instance.ItemDB);
        m_OrderPresenter.OnNewOrder();
    }

    public Vector3 GetPosition(){
        return transform.position;
    }

    public bool IsEmpty(){
        return m_CurrentTotalStock <= 0;
    }

    public int AddItem(int amount){
        throw new NotImplementedException();
    }

    public int GetItem(int requiredAmount){
        throw new NotImplementedException();
    }

    public string GetItemId(){
        throw new NotImplementedException();
    }
}
