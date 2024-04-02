using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxShopView : MonoBehaviour
{
    [Header("Item data")]
    [SerializeField] Image ItemIcon;
    [SerializeField] TextMeshProUGUI ItemPriceText;
    [Header("Amount to buy controls")]
    [SerializeField] TextMeshProUGUI ItemAmountText;
    [SerializeField] Button AddAmountButton;
    [SerializeField] Button RestAmountButton;
    [SerializeField] TextMeshProUGUI ItemAmountCostText;
    private string m_ItemId;
    private int m_ItemAmount;
    public int ItemAmountToBuy => m_ItemAmount;
    public event Action OnAmountChanged;

    void Awake(){
        AddAmountButton.onClick.AddListener(() => {
            AddAmountToBuy();
            CheckToBlockAmountButtons();
            OnAmountChanged();
        });

        RestAmountButton.onClick.AddListener(() => {
            RestAmountToBuy();
            CheckToBlockAmountButtons();
            OnAmountChanged();    
        });
    }

    public void SetItemInfo(Item item){
        ItemIcon.sprite = item.Icon;
        m_ItemId = item.Id;
        ItemPriceText.SetText($"{item.BuyingPrice},00 UD");
        CheckToBlockAmountButtons();
    }

    public void Reset(){
        m_ItemAmount = 0;
        SetAmountToBuy();
        SetMoneyAmountToCost();
        CheckToBlockAmountButtons();
    }

    void AddAmountToBuy(){
        m_ItemAmount++;
        SetAmountToBuy();
        SetMoneyAmountToCost();
    }

    void RestAmountToBuy(){
        m_ItemAmount--;
        SetAmountToBuy();
        SetMoneyAmountToCost();
    }

    void SetAmountToBuy(){
        ItemAmountText.SetText($"{m_ItemAmount}");
    }

    void SetMoneyAmountToCost(){
        ItemAmountCostText.SetText($"{m_ItemAmount * WarehouseStorage.Instance.ItemDB.GetItem(m_ItemId).BuyingPrice}");
    }

    void CheckToBlockAmountButtons(){
        CheckToBlockAddButton();
        CheckToBlockRestButton();
    }

    void CheckToBlockAddButton(){
        ItemStorage itemStorage = WarehouseStorage.Instance.ItemStorages.Find(storage => storage.ItemId == m_ItemId);
        AddAmountButton.interactable = m_ItemAmount < itemStorage.ItemMaxStock - itemStorage.ItemStock;
    }

    void CheckToBlockRestButton(){
        RestAmountButton.interactable = m_ItemAmount >= 1;
    }
}
