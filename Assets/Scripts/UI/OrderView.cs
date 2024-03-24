using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderView : MonoBehaviour
{
    [SerializeField] GameObject itemBoxPrefab;
    [SerializeField] GameObject itemBoxListPanel;
    private List<ItemBoxView> m_ItemBoxViewPool = new List<ItemBoxView>();
    private Dictionary<string, ItemBoxView> m_ItemBoxViewDictionary = new Dictionary<string, ItemBoxView>();
    [SerializeField] TextMeshProUGUI rewardText;

    void Awake(){
        InitializePool();
    }

    void InitializePool(){
        for(int i=0; i < 3; i++){
            AddItemBoxToPool();
        }
    }

    ItemBoxView AddItemBoxToPool(){
        ItemBoxView itemBoxView = Instantiate(itemBoxPrefab, itemBoxListPanel.transform).GetComponent<ItemBoxView>();
        m_ItemBoxViewPool.Add(itemBoxView);
        itemBoxView.gameObject.SetActive(false);
        return itemBoxView;
    }

    ItemBoxView GetItemBoxFromPool(){
        foreach(ItemBoxView itemBoxView in m_ItemBoxViewPool)
            if(!itemBoxView.gameObject.activeInHierarchy)
                return itemBoxView;
        
        return AddItemBoxToPool();
    }

    void ResetPool(){
        foreach(ItemBoxView itemBoxView in m_ItemBoxViewPool)
            itemBoxView.gameObject.SetActive(false);
    }

    public void SetOrderItems(List<Order.OrderEntry> orderEntry){
        m_ItemBoxViewDictionary.Clear();
        ResetPool();
        
        for(int i=0; i < orderEntry.Count; i++){
            ItemBoxView itemBoxView = GetItemBoxFromPool();
            itemBoxView.SetItemIcon(WarehouseStorage.Instance.ItemDB.GetItem(orderEntry[i].ItemId));
            itemBoxView.SetItemText(orderEntry[i].CurrentStock, orderEntry[i].OrderedStock);
            itemBoxView.gameObject.SetActive(true);
            m_ItemBoxViewDictionary.Add(orderEntry[i].ItemId, itemBoxView);
        }
    }

    public void SetReward(int amount){
        rewardText.SetText($"Reward: {amount}");
    }

    public void UpdateItemsList(Order.OrderEntry orderEntry){
        if(m_ItemBoxViewDictionary.ContainsKey(orderEntry.ItemId)){
            m_ItemBoxViewDictionary.TryGetValue(orderEntry.ItemId, out ItemBoxView itemBoxView);
            itemBoxView.SetItemText(orderEntry.CurrentStock, orderEntry.OrderedStock);
        }
    }
}
