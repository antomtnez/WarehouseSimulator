using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxShopView : MonoBehaviour
{
    [SerializeField] Image ItemIcon;
    [SerializeField] TextMeshProUGUI ItemPriceText;
    [SerializeField] TextMeshProUGUI ItemAmountText;
    [SerializeField] Button AddAmountButton;
    [SerializeField] Button RestAmountButton;
    private int m_ItemAmount;
    public int ItemAmountToBuy => m_ItemAmount;
    public event Action OnAmountChanged;

    void Awake(){
        AddAmountButton.onClick.AddListener(() => {
            AddAmountToBuy();
            OnAmountChanged();
        });
        RestAmountButton.onClick.AddListener(() => {
            RestAmountToBuy();
            OnAmountChanged();    
        });
    }

    public void SetItemInfo(Item item){
        ItemIcon.sprite = item.Icon;
        ItemPriceText.SetText($"{item.BuyingPrice},00");
    }

    void AddAmountToBuy(){
        m_ItemAmount++;
        SetAmountToBuy();
    }

    void RestAmountToBuy(){
        m_ItemAmount--;
        SetAmountToBuy();
    }

    void SetAmountToBuy(){
        ItemAmountText.SetText($"{m_ItemAmount}");
    }
}
