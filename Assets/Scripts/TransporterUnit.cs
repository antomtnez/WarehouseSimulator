using UnityEngine;

/// <summary>
/// Subclass of Unit that will transport resource from a Resource Pile back to Base.
/// </summary>
public class TransporterUnit : Unit
{
    public int MaxAmountTransported = 0;
    private int m_AmountTransported;
    private IStorageInteractable m_CurrentTransportTarget;
    [SerializeField] ItemPile m_ItemPileTransporting;
    private float timeRemaining = 0f;
    private bool m_IsLoaded = false;

    new void Update(){
        base.Update();
        UpdateSpeed();
    }

    // We override the GoTo function to remove the current transport target, as any go to order will cancel the transport
    public override void GoTo(Vector3 position)
    {
        base.GoTo(position);
        m_CurrentTransportTarget = null;
    }
    
    protected override void BuildingInRange(){
        if (m_Target == DropPoint.Instance){
            //we arrive at the base, unload!
            if (!m_ItemPileTransporting.IsEmpty())
                DroppingItems();

            if (!m_IsLoaded)
                GoBackToItemRack();
        }else{
            if (!m_Target.IsEmpty())
                LoadingItems();
            
            if (m_IsLoaded)
                GoToDropItem();
        }
    }

    void DroppingItems(){
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
        }else{
            if(m_Target == DropPoint.Instance){
                m_Target.AddItem(m_ItemPileTransporting.ItemId, m_ItemPileTransporting.ItemStock);    
            }else{
                m_Target.AddItem(m_ItemPileTransporting.ItemStock);
            }
            m_IsLoaded = false;
        }
    }

    void RemoveLoadedItems(){
        m_ItemPileTransporting = new ItemPile();
    }

    //we go back to the item rack we came from
    void GoBackToItemRack(){        
        ResetLoadingCountdown();

        GoTo(m_CurrentTransportTarget);
    }

    void ResetLoadingCountdown(){
        timeRemaining = MaxAmountTransported * 1f;
    }

    void LoadingItems(){
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
        }else{
            m_ItemPileTransporting.Init(m_Target.GetItemId());
            m_ItemPileTransporting.AddItem(m_Target.GetItem(m_AmountTransported)); 
            m_IsLoaded = true;
        }
    }

    void GoToDropItem(){
        ResetDroppingCountdown();

        m_CurrentTransportTarget = m_Target;
        GoTo(DropPoint.Instance);
    }

    void ResetDroppingCountdown(){
        timeRemaining = m_ItemPileTransporting.ItemStock * 1;
    }

    void UpdateSpeed(){
        m_Agent.speed = m_IsLoaded ? LoadedSpeed : Speed;
    }
}