using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TotalPriceText;
    [SerializeField] GameObject ItemBoxShopContainer;
    [SerializeField] GameObject ItemBoxShopPrefab;
    [SerializeField] Button BuyButton;
    private Dictionary<string, ItemBoxShopView> m_ItemBoxShopList = new Dictionary<string, ItemBoxShopView>();
    private List<Shop.ShopEntry> m_ShopEntries = new List<Shop.ShopEntry>();
    public event Action OnShopOrderChanged;

    public void Init(){
        foreach(Item item in WarehouseStorage.Instance.ItemDB.ItemTypes){
            ItemBoxShopView itemBoxShopView = Instantiate(ItemBoxShopPrefab, ItemBoxShopContainer.transform).GetComponent<ItemBoxShopView>();
            itemBoxShopView.SetItemInfo(item);
            itemBoxShopView.OnAmountChanged += UpdateShopEntries;

            m_ItemBoxShopList.Add(item.Id, itemBoxShopView);
            m_ShopEntries.Add(new Shop.ShopEntry(){
                ItemId = item.Id,
                Amount = itemBoxShopView.ItemAmountToBuy,
                ItemPrice = item.BuyingPrice
            });
        }
    }

    public void ResetView(){
        foreach(Shop.ShopEntry shopEntry in m_ShopEntries){
            if(m_ItemBoxShopList.TryGetValue(shopEntry.ItemId, out ItemBoxShopView itemBoxShopView)){
                itemBoxShopView.Reset();
                shopEntry.Amount = itemBoxShopView.ItemAmountToBuy;
            }
        }
        OnShopOrderChanged();
    }

    void UpdateShopEntries(){
        foreach(Shop.ShopEntry shopEntry in m_ShopEntries){
            if(m_ItemBoxShopList.TryGetValue(shopEntry.ItemId, out ItemBoxShopView itemBoxShopView))
                shopEntry.Amount = itemBoxShopView.ItemAmountToBuy;
        }
        OnShopOrderChanged();
    }

    public List<Shop.ShopEntry> GetShopOrder(){
        return m_ShopEntries;
    }

    public void SetTotalPrice(int amount){
        TotalPriceText.SetText($"{amount},00");
    }

    public void CanYouBuy(bool moneyOverPrice){
        BuyButton.interactable = moneyOverPrice;

        Color totalPriceTextColor;
        if(moneyOverPrice){
            ColorUtility.TryParseHtmlString("#4d4d4d", out totalPriceTextColor); 
        }else{
            ColorUtility.TryParseHtmlString("#f63900", out totalPriceTextColor);
        }

        TotalPriceText.color = totalPriceTextColor;
    }
}