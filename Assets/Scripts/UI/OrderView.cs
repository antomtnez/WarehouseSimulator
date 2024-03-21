using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderView : MonoBehaviour
{
    [SerializeField] GameObject itemBoxPrefab;
    [SerializeField] GameObject itemBoxList;
    [SerializeField] TextMeshProUGUI rewardText;

    public void SetOrderItems(List<Inventory.InventoryEntry> orderEntry, List<Inventory.InventoryEntry> inventoryEntry){
        for(int i=0; i < orderEntry.Count; i++){
            ItemBoxView itemBoxView = Instantiate(itemBoxPrefab, itemBoxList.transform).GetComponent<ItemBoxView>();
            itemBoxView.SetItemIcon(GameManager.Instance.ItemDB.GetItem(orderEntry[i].ItemId));
            itemBoxView.SetItemText(inventoryEntry[i].Count, orderEntry[i].Count);
        }
    }

    public void SetReward(int amount){
        rewardText.SetText($"Reward: {amount}");
    }
}
