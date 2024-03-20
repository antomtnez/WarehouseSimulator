using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Subclass of Unit that will transport resource from a Resource Pile back to Base.
/// </summary>
public class TransporterUnit : Unit
{
    public int MaxAmountTransported = 1;

    private Inventory m_CurrentTransportTarget;
    private Inventory.InventoryEntry m_Transporting = new Inventory.InventoryEntry();

    // We override the GoTo function to remove the current transport target, as any go to order will cancel the transport
    public override void GoTo(Vector3 position)
    {
        base.GoTo(position);
        m_CurrentTransportTarget = null;
    }
    
    protected override void BuildingInRange()
    {
        if (m_Target == DropPoint.Instance)
        {
            //we arrive at the base, unload!
            if (m_Transporting.Count > 0)
                m_Target.AddItem(m_Transporting.ItemId, m_Transporting.Count);

            //we go back to the building we came from
            GoTo(m_CurrentTransportTarget);
            m_Transporting.Count = 0;
            m_Transporting.ItemId = "";
        }
        else
        {
            if (m_Target.InventoryEntryList.Count > 0)
            {
                m_Transporting.ItemId = m_Target.InventoryEntryList[0].ItemId;
                m_Transporting.Count = m_Target.GetItem(m_Transporting.ItemId, MaxAmountTransported);
                m_CurrentTransportTarget = m_Target;
                GoTo(DropPoint.Instance);
            }
        }
    }
}