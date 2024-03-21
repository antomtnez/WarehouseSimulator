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

    private WarehouseStoragePresenter warehouseStoragePresenter;

    
    void Awake(){
        Instance = this;
        m_ItemDatabase.Init();
    }

    void Start(){
        Init();
        warehouseStoragePresenter = new WarehouseStoragePresenter(FindObjectOfType<WarehouseStorageView>());
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
}
