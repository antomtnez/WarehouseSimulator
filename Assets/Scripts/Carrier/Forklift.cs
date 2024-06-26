using System.Collections;
using UnityEngine;

public class Forklift : Carrier
{
    [SerializeField] ItemPile m_ItemPileTransporting;
    public ItemPile ItemPileTransporting => m_ItemPileTransporting;
    [SerializeField] int m_AmountRequiredToTransport = 3;
    public int AmountRequiredToTransport => m_AmountRequiredToTransport;
    private int m_TimeToManagePileItems = 1;

    void Start(){
        m_CurrentState = new IdleState(this);
    }

    public override void DropItems(){
        StartCoroutine(DelayedDropItems());
    }

    public override void ReturnItemsToRack(){
        StartCoroutine(DelayedReturnItems());
    }

    public override void LoadItems(){
        if(CanIGetItemsFromStorage()){
            StartCoroutine(DelayedLoadItems());
        }else{
            ChangeState(new IdleState(this));
        }
    }

    public override bool IsEmpty(){
        return m_ItemPileTransporting.IsEmpty();
    }

    private IEnumerator DelayedDropItems(){
        float countdown = 0;
        while(countdown < m_TimeToManagePileItems){
            yield return new WaitForSeconds(0.1f);
            countdown += 0.25f;
        }

        m_ItemPileTransporting.AddItem(m_StorageTarget.AddItem(m_ItemPileTransporting.ItemId, ItemPileTransporting.GetAllPile()));
        OnTaskFinishedActionCall();
    }

    private IEnumerator DelayedReturnItems(){
        float countdown = 0;
        while(countdown < m_TimeToManagePileItems){
            yield return new WaitForSeconds(0.1f);
            countdown += 0.25f;
        }

        m_StorageTarget.AddItem(m_ItemPileTransporting.GetAllPile());
        WarehouseStorage.Instance.UpdateItemStorage(m_StorageTarget.GetItemId());
        OnTaskFinishedActionCall();
    }

    private IEnumerator DelayedLoadItems(){
        float countdown = 0;
        while(countdown < m_TimeToManagePileItems){
            yield return new WaitForSeconds(0.2f);
            countdown += 0.25f;
        }

        if(m_ItemPileTransporting.ItemId != m_StorageTarget.GetItemId())
            m_ItemPileTransporting.Init(m_StorageTarget.GetItemId());
        
        m_ItemPileTransporting.AddItem(m_StorageTarget.GetItem(m_AmountRequiredToTransport));
        WarehouseStorage.Instance.UpdateItemStorage(m_StorageTarget.GetItemId());
        OnTaskFinishedActionCall();
    }

    public void SetAmountToLoadByForklift(int amount){
        m_AmountRequiredToTransport = amount;
    }

    public override string GetName(){
        return "Forklift";
    }

    public override string GetData(){
        return ($"Estado: {m_CurrentStatus}");
    }

    public override Object GetContent(){
        return this;
    }
}
