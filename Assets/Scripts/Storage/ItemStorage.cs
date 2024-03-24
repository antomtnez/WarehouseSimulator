using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStorage
{
    public string ItemId;
    [SerializeField] private int m_ItemStock = 0;
    [SerializeField] private int m_ItemMaxStock = 0;
    public int ItemStock => m_ItemStock;
    public int ItemMaxStock => m_ItemMaxStock;
    [SerializeField] List<ItemRack> m_ItemRacks = new List<ItemRack>();

    public ItemStorage(string ItemId){
        this.ItemId = ItemId;
        SetRacks();
        SetMaxStock();
        SetStock();
    }

    void SetRacks(){
        foreach(ItemRack itemRack in GameObject.FindObjectsOfType<ItemRack>()){
            if(itemRack.ItemId == ItemId){
                itemRack.Init();
                m_ItemRacks.Add(itemRack);
            }
        }       
    }

    void SetMaxStock(){
        m_ItemMaxStock = m_ItemRacks.Count * m_ItemRacks[0].ItemMaxStock;
    }

    public void SetStock(){
        m_ItemStock = 0;
        foreach(ItemRack itemRack in m_ItemRacks)
            m_ItemStock += itemRack.ItemStock;
    }

    public int AddItem(int amount){
        for(int i=0; i < m_ItemRacks.Count; i++){
            amount = m_ItemRacks[i].AddItem(amount);
            if(ItemStock == ItemMaxStock || amount == 0) break;
        }
        SetStock();
        return amount;
    }

    public int GetItem(int requestAmount){
        int amountCanGet = Mathf.Min(requestAmount, ItemStock);

        for(int i = m_ItemRacks.Count-1; i >= 0; i--){
            amountCanGet -= m_ItemRacks[i].GetItem(amountCanGet > m_ItemRacks[0].ItemMaxStock ? m_ItemRacks[0].ItemMaxStock : amountCanGet);
            if(amountCanGet == 0) break;
        }

        SetStock();
        return requestAmount - amountCanGet;
    }
}
