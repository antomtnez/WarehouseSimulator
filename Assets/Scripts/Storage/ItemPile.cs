using UnityEngine;

public class ItemPile : MonoBehaviour
{
    private string ItemId;
    GameObject ItemPileDisplay;
    [SerializeField]private int m_ItemStock = 0;
    [SerializeField]private int m_ItemMaxStock = 0;
    public int ItemStock => m_ItemStock;
    public int ItemMaxStock => m_ItemMaxStock;

    public void Init(string ItemId){
        this.ItemId = ItemId;
        m_ItemMaxStock = WarehouseStorage.Instance.ItemDB.GetItem(ItemId).MaxStockableInAPile; 
        if(ItemPileDisplay == null)
           Instantiate(WarehouseStorage.Instance.ItemDB.GetItem(ItemId).ItemGameObject, transform);
        
        //SetDisplay();
    }

    public int AddItem(int amount){
        if (ItemStock == ItemMaxStock)
            return amount;

        int addedAmount = Mathf.Min(ItemMaxStock - ItemStock, amount);

        m_ItemStock += addedAmount;
        SetDisplay();
        return amount - addedAmount;
    }

    public int GetItem(int requestAmount){
        int amount = Mathf.Min(requestAmount, ItemStock);
        m_ItemStock -= amount;
        return amount;
    }

    void SetDisplay(){
        ItemPileDisplay.SetActive(ItemStock >= 1);
    }
}
