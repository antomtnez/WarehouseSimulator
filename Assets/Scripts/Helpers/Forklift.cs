using UnityEngine;

public class Forklift : Carrier
{
    [SerializeField] ItemPile m_ItemPileTransporting;
    public ItemPile ItemPileTransporting => m_ItemPileTransporting;
    public int m_AmountRequiredToTransport = 3;

    void Start(){
        m_CurrentState = new IdleState(this);
    }

    public override void DropItems(){
        m_ItemPileTransporting.AddItem(m_StorageTarget.AddItem(m_ItemPileTransporting.ItemId, ItemPileTransporting.GetAllPile()));    
    }

    public override void ReturnItemsToRack(){
        m_StorageTarget.AddItem(m_ItemPileTransporting.GetAllPile());
        WarehouseStorage.Instance.UpdateItemStorage(m_StorageTarget.GetItemId());
    }

    public override void LoadItems(){
        m_ItemPileTransporting.Init(m_StorageTarget.GetItemId());
        m_ItemPileTransporting.AddItem(m_StorageTarget.GetItem(m_AmountRequiredToTransport));
        WarehouseStorage.Instance.UpdateItemStorage(m_StorageTarget.GetItemId());
    }

    public override bool IsEmpty(){
        return m_ItemPileTransporting.IsEmpty();
    }
}
