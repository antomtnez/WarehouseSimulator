using System;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseStorage : MonoBehaviour
{
    public static WarehouseStorage Instance;
    [SerializeField] ItemDatabase m_ItemDatabase;
    public ItemDatabase ItemDB => m_ItemDatabase;
    [SerializeField] List<ItemStorage> m_ItemsStorages = new List<ItemStorage>();
    public List<ItemStorage> ItemStorages => m_ItemsStorages;

    private WarehouseStoragePresenter m_WarehouseStoragePresenter;

    
    void Awake(){
        Instance = this;
        m_ItemDatabase.Init();
        Init();
        m_WarehouseStoragePresenter = new WarehouseStoragePresenter(FindObjectOfType<WarehouseStorageView>());
    }

    void Start(){
        StartStoragesWithFullStock();
    }

    void Init(){
        foreach(Item item in m_ItemDatabase.ItemTypes){
            try{
                m_ItemsStorages.Add(new ItemStorage(item.Id));
            }catch(Exception e){
                Debug.LogWarning(e);
            }
        }
    }

    void StartStoragesWithFullStock(){
        foreach(ItemStorage itemStorage in m_ItemsStorages){
            itemStorage.AddItem(itemStorage.ItemMaxStock);
            m_WarehouseStoragePresenter.OnItemStockChanged(itemStorage);
        }
    }

    public void UpdateItemStorage(string ItemId){
        int found = m_ItemsStorages.FindIndex(itemStorage => itemStorage.ItemId == ItemId);
        if(found >= 0){
            m_ItemsStorages[found].SetStock();
            m_WarehouseStoragePresenter.OnItemStockChanged(m_ItemsStorages[found]);
        }
    }
}
