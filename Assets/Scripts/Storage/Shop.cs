using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    public class ShopEntry{
        public string ItemId;
        public int Amount;
        public int ItemPrice;
    }
    [SerializeField] List<ShopEntry> shopOrder = new List<ShopEntry>();
    private int m_TotalPrice = 0;
    public int TotalPrice => m_TotalPrice;
    private ShopPresenter m_ShopPresenter;

    void Start(){
        Init();
        m_ShopPresenter = new ShopPresenter(this, FindObjectOfType<ShopView>());
    }

    void Init(){
        foreach(Item item in WarehouseStorage.Instance.ItemDB.ItemTypes){
            shopOrder.Add(new ShopEntry(){
                ItemId = item.Id,
                Amount = 0,
                ItemPrice = item.BuyingPrice
            });
        }
    }

    public void SetShopOrder(List<ShopEntry> shopEntries){
        shopOrder = shopEntries;
        UpdateTotalPrice();
    }

    void UpdateTotalPrice(){
        m_TotalPrice = 0;
        foreach(ShopEntry shopEntry in shopOrder)
            m_TotalPrice += (shopEntry.Amount * shopEntry.ItemPrice);
    }
}
