using System.Collections.Generic;
using UnityEngine;

public class ItemRack : MonoBehaviour, IStorageInteractable
{
    [SerializeField] Item m_Item;
    [SerializeField]private int m_ItemStock = 0;
    [SerializeField]private int m_ItemMaxStock = 0;
    public string ItemId => m_Item.Id;
    public int ItemStock => m_ItemStock;
    public int ItemMaxStock => m_ItemMaxStock;
    [SerializeField] List<ItemPile> m_ItemPiles = new List<ItemPile>();

    public void Init(){
        SetRackPiles();
        SetMaxStock();
        SetStock();
    }

    void SetRackPiles(){
        foreach(ItemPile itemPile in transform.GetComponentsInChildren<ItemPile>()){
            itemPile.Init(m_Item.Id);
            m_ItemPiles.Add(itemPile);
        }
    }

    //We give the inventory space according to the piles we have available 
    //and the maximum amount that the item allows us to stack in a pile.
    void SetMaxStock(){
        m_ItemMaxStock = m_ItemPiles[0].ItemMaxStock * m_ItemPiles.Count;
    }

    void SetStock(){
        m_ItemStock = 0;
        foreach(ItemPile itemPile in m_ItemPiles)
            m_ItemStock += itemPile.ItemStock;
    }

    public int AddItem(int amount){
        for(int i=0; i < m_ItemPiles.Count; i++){
            amount = m_ItemPiles[i].AddItem(amount);
            if(ItemStock == ItemMaxStock || amount == 0) break;
        }
        SetStock();
        return amount;
    }

    public int GetItem(int requestAmount){
        int amount = Mathf.Min(requestAmount, ItemStock);
        requestAmount = 0;
        for(int i = m_ItemPiles.Count-1; i >= 0; i--){
            requestAmount += m_ItemPiles[i].GetItem(amount);
            if(requestAmount == amount) break;
        }
        SetStock();
        return requestAmount;
    }

    public Vector3 GetPosition(){
        for(int i = m_ItemPiles.Count-1; i > 0; i--){
            if(!m_ItemPiles[i].IsEmpty()){
                return m_ItemPiles[i].transform.position;
            }
        }

        return transform.position;
    }

    public bool IsEmpty(){
        return m_ItemStock <= 0;
    }

    public int AddItem(string itemId, int amount){
        throw new System.NotImplementedException();
    }

    public string GetItemId(){
        return m_Item.Id;
    }
}
