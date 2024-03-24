using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Forklift : Carrier
{
    [SerializeField] ItemPile m_ItemPileTransporting;
    public ItemPile ItemPileTransporting => m_ItemPileTransporting;
    public int m_AmountRequiredToTransport = 3;
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
        StartCoroutine(DelayedLoadItems());
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

        m_ItemPileTransporting.Init(m_StorageTarget.GetItemId());
        m_ItemPileTransporting.AddItem(m_StorageTarget.GetItem(m_AmountRequiredToTransport));
        WarehouseStorage.Instance.UpdateItemStorage(m_StorageTarget.GetItemId());
        OnTaskFinishedActionCall();
    }
}
