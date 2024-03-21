using System.Collections.Generic;
using UnityEngine;

public class ItemRack : MonoBehaviour
{
    [SerializeField] Item m_Item;
    [SerializeField]private int m_ItemStock = 0;
    [SerializeField]private int m_ItemMaxStock = 0;
    public string ItemId => m_Item.Id;
    public int ItemStock => m_ItemStock;
    public int ItemMaxStock => m_ItemMaxStock;
    [SerializeField] List<ItemPile> m_ItemPilesAvaliables = new List<ItemPile>();

    public void Init(){
        SetRackPiles();
        SetMaxStock();
        SetStock();
    }

    void SetRackPiles(){
        foreach(ItemPile itemPile in transform.GetComponentsInChildren<ItemPile>()){
            itemPile.Init(m_Item.Id);
            m_ItemPilesAvaliables.Add(itemPile);
        }
    }

    //We give the inventory space according to the piles we have available 
    //and the maximum amount that the item allows us to stack in a pile.
    void SetMaxStock(){
        m_ItemMaxStock = m_ItemPilesAvaliables[0].ItemMaxStock * m_ItemPilesAvaliables.Count;
    }

    void SetStock(){
        m_ItemStock = 0;
        foreach(ItemPile itemPile in m_ItemPilesAvaliables)
            m_ItemStock += itemPile.ItemStock;
    }

    public int AddItem(int amount){
        for(int i=0; i < m_ItemPilesAvaliables.Count || ItemStock == ItemMaxStock || amount == 0; i++){
            amount = m_ItemPilesAvaliables[i].AddItem(amount);
        }
        SetStock();
        return amount;
    }

    public int GetItem(int requestAmount){
        int amount = Mathf.Min(requestAmount, ItemStock);
        requestAmount = 0;
        for(int i = m_ItemPilesAvaliables.Count; i <= 0 || requestAmount == amount; i--){
            requestAmount += m_ItemPilesAvaliables[i].GetItem(amount);
        }
        SetStock();
        return requestAmount;
    }
}
