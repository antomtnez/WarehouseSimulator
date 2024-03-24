using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderView : MonoBehaviour
{
    [SerializeField] GameObject itemBoxPrefab;
    [SerializeField] GameObject itemBoxListPanel;
    private Dictionary<string, ItemBoxView> m_ItemBoxViewDictionary = new Dictionary<string, ItemBoxView>();
    [SerializeField] TextMeshProUGUI rewardText;

    public void SetOrderItems(List<Order.OrderEntry> orderEntry){
        m_ItemBoxViewDictionary.Clear();

        for(int i=0; i < orderEntry.Count; i++){
            ItemBoxView itemBoxView = Instantiate(itemBoxPrefab, itemBoxListPanel.transform).GetComponent<ItemBoxView>();
            itemBoxView.SetItemIcon(WarehouseStorage.Instance.ItemDB.GetItem(orderEntry[i].ItemId));
            itemBoxView.SetItemText(orderEntry[i].CurrentStock, orderEntry[i].OrderedStock);
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
