using System.Collections.Generic;
using UnityEngine;

public class WarehouseStorageView : MonoBehaviour
{
    [SerializeField] GameObject ItemBoxPrefab;
    [SerializeField] GameObject ItemBoxListPanel;
    private List<ItemBoxView> m_ItemBoxViewList = new List<ItemBoxView>();
    private Dictionary<string, ItemBoxView> m_ItemBoxViewDictionary = new Dictionary<string, ItemBoxView>();
    
    public void SetItemCounters(List<ItemStorage> itemsStorages){
        for(int i=0; i < itemsStorages.Count; i++){
            m_ItemBoxViewList.Add(Instantiate(ItemBoxPrefab, ItemBoxListPanel.transform).GetComponent<ItemBoxView>());
            m_ItemBoxViewDictionary.Add(itemsStorages[i].ItemId, m_ItemBoxViewList[i]);
            m_ItemBoxViewList[i].SetItemIcon(WarehouseStorage.Instance.ItemDB.GetItem(itemsStorages[i].ItemId));
            m_ItemBoxViewList[i].SetItemText(itemsStorages[i].ItemStock, itemsStorages[i].ItemMaxStock);
        }
    }

    public void UpdateItemCounter(ItemStorage itemStorage){
        m_ItemBoxViewDictionary.TryGetValue(itemStorage.ItemId, out ItemBoxView itemBoxView);
        itemBoxView.SetItemText(itemStorage.ItemStock, itemStorage.ItemMaxStock);
    }
}
