using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderView : MonoBehaviour
{
    [SerializeField] GameObject itemBoxPrefab;
    [SerializeField] GameObject itemBoxListPanel;
    private Dictionary<string, ItemBoxView> m_ItemBoxViewDictionary = new Dictionary<string, ItemBoxView>();
    [SerializeField] TextMeshProUGUI rewardText;

    public void SetOrderItems(List<Inventory.InventoryEntry> orderEntry, List<Inventory.InventoryEntry> inventoryEntry){
        for(int i=0; i < orderEntry.Count; i++){
            ItemBoxView itemBoxView = Instantiate(itemBoxPrefab, itemBoxListPanel.transform).GetComponent<ItemBoxView>();
            itemBoxView.SetItemIcon(WarehouseStorage.Instance.ItemDB.GetItem(orderEntry[i].ItemId));
            itemBoxView.SetItemText(inventoryEntry[i].Count, orderEntry[i].Count);
            m_ItemBoxViewDictionary.Add(orderEntry[i].ItemId, itemBoxView);
        }
    }

    public void SetReward(int amount){
        rewardText.SetText($"Reward: {amount}");
    }

    public void UpdateItemsList(Inventory.InventoryEntry orderEntry, Inventory.InventoryEntry itemEntry){
        if(m_ItemBoxViewDictionary.ContainsKey(itemEntry.ItemId)){
            m_ItemBoxViewDictionary.TryGetValue(itemEntry.ItemId, out ItemBoxView itemBoxView);
            itemBoxView.SetItemText(itemEntry.Count, orderEntry.Count);
        }
    }
}
