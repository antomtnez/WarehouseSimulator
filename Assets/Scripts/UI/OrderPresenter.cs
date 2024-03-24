public class OrderPresenter
{
    private DropPoint m_DropPoint;
    private OrderView m_OrderView;

    public OrderPresenter(DropPoint dropPoint, OrderView orderView){
        m_DropPoint = dropPoint;
        m_OrderView = orderView;
    } 

    public void OnNewOrder(){
        m_OrderView.SetOrderItems(m_DropPoint.DropOrder.OrderInventory);
        m_OrderView.SetReward(m_DropPoint.DropOrder.Reward);
    }

    public void OnInventoryChanged(string ItemId){
        int found = m_DropPoint.DropOrder.OrderInventory.FindIndex(item => item.ItemId == ItemId);
        if(found != -1)
            m_OrderView.UpdateItemsList(m_DropPoint.DropOrder.OrderInventory[found]);
    }
}
