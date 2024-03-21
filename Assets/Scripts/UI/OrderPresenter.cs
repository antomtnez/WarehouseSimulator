public class OrderPresenter
{
    private DropPoint m_DropPoint;
    private OrderView m_OrderView;

    public OrderPresenter(DropPoint dropPoint, OrderView orderView){
        m_DropPoint = dropPoint;
        m_OrderView = orderView;
        InitializeView();
    } 

    void InitializeView(){
        m_OrderView.SetOrderItems(m_DropPoint.DropOrder.OrderInventory, m_DropPoint.InventoryEntryList);
        m_OrderView.SetReward(m_DropPoint.DropOrder.Reward);
    }
}
