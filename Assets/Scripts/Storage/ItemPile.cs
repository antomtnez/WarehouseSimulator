using UnityEngine;

public class ItemPile : MonoBehaviour
{
    private string m_ItemId;
    private GameObject ItemPileDisplay;
    [SerializeField]private int m_ItemStock = 0;
    [SerializeField]private int m_ItemMaxStock = 0;
    public int ItemStock => m_ItemStock;
    public int ItemMaxStock => m_ItemMaxStock;
    public string ItemId => m_ItemId;

    public void Init(string ItemId){
        m_ItemId = ItemId;
        m_ItemMaxStock = WarehouseStorage.Instance.ItemDB.GetItem(ItemId).MaxStockableInAPile; 
        if(WarehouseStorage.Instance.ItemDB.GetItem(ItemId).ItemGameObject != ItemPileDisplay)
            Destroy(ItemPileDisplay);
        
        ItemPileDisplay = Instantiate(WarehouseStorage.Instance.ItemDB.GetItem(ItemId).ItemGameObject, transform);
            
        SetDisplay();
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
        SetDisplay();
        return amount;
    }

    public int GetAllPile(){
        int amount = m_ItemStock;
        m_ItemStock = 0;
        SetDisplay();
        return amount;
    }

    void SetDisplay(){
        ItemPileDisplay.SetActive(!IsEmpty());
    }

    public bool IsEmpty(){
        return m_ItemStock <= 0;
    }
}
