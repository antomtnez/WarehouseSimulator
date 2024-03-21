using UnityEngine;

/// <summary>
/// Subclass of Unit that will transport resource from a Resource Pile back to Base.
/// </summary>
public class TransporterUnit : Unit
{
    public int MaxAmountTransported = 1;

    private Inventory m_CurrentTransportTarget;
    [SerializeField] Inventory.InventoryEntry m_Transporting = new Inventory.InventoryEntry();
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
            if (m_Transporting.Count > 0)
                DroppingItems();

            if(!m_IsLoaded)
                GoBackToItemRack();
        }else{
            if(m_Transporting.Count > 0)
                DroppingItems();

            if (m_Target.InventoryEntryList.Count > 0)
                LoadingItems();
            
            if(m_IsLoaded)
                GoToDropItem();
        }
    }

    void DroppingItems(){
        
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
        }else{
            m_Target.AddItem(m_Transporting.ItemId, m_Transporting.Count);
            m_IsLoaded = false;
        }
    }

    void RemoveLoadedItems(){
        m_Transporting.Count = 0;
        m_Transporting.ItemId = "";
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
            m_Transporting.ItemId = m_Target.InventoryEntryList[0].ItemId;
            m_Transporting.Count = m_Target.GetItem(m_Transporting.ItemId, MaxAmountTransported);
            m_IsLoaded = true;
        }
    }

    void GoToDropItem(){
        ResetDroppingCountdown();

        m_CurrentTransportTarget = m_Target;
        GoTo(DropPoint.Instance);
    }

    void ResetDroppingCountdown(){
        timeRemaining = m_Transporting.Count * 1;
    }

    void UpdateSpeed(){
        m_Agent.speed = m_IsLoaded ? LoadedSpeed : Speed;
    }
}