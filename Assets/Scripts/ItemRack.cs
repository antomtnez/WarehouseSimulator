using System.Collections.Generic;
using UnityEngine;

public class ItemRack : Inventory
{
    public Item Item;
    public float ProductionSpeed = 0.75f;
    [SerializeField] float m_CurrentProduction = 0.0f;

    [SerializeField] List<RackPile> m_RackPilesAvaliables = new List<RackPile>();

    void Start(){
        FindRackPiles();
        SetInventorySpace();
    }

    void Update()
    {
        Production();
    }

    void Production(){
        if (m_CurrentProduction > 1.0f)
        {
            int amountToAdd = Mathf.FloorToInt(m_CurrentProduction);
            int leftOver = AddItem(Item.Id, amountToAdd);

            m_CurrentProduction = m_CurrentProduction - amountToAdd + leftOver;
            UpdateRackPiles();
        }
        
        if (m_CurrentProduction < 1.0f)
        {
            m_CurrentProduction += ProductionSpeed * Time.deltaTime;
        }
    }

    void FindRackPiles(){
        foreach(RackPile rackPile in transform.GetComponentsInChildren<RackPile>()){
            m_RackPilesAvaliables.Add(rackPile);
            rackPile.Init(Item);
        }
    }

    //We give the inventory space according to the piles we have available 
    //and the maximum amount that the item allows us to stack in a pile.
    void SetInventorySpace(){
        InventorySpace = Item.MaxStockableInAPile * m_RackPilesAvaliables.Count;
    }

    void UpdateRackPiles(){
        int rackPilesWithStock = m_CurrentAmount / Item.MaxStockableInAPile;
        rackPilesWithStock += (m_CurrentAmount % Item.MaxStockableInAPile != 0) ? 1 : 0;

        for(int i=0; i < m_RackPilesAvaliables.Count; i++){
            m_RackPilesAvaliables[i].ActivePileStockGO(i < rackPilesWithStock);
        }
    }

    public RackPile GetAPileWithStock(){
        for(int i=0; i<m_RackPilesAvaliables.Count; i++){
            if(m_RackPilesAvaliables[i].IsEmpty())
                return m_RackPilesAvaliables[i];
        }
        return null;
    }
}
