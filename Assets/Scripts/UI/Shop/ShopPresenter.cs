public class ShopPresenter{
    private Shop m_Shop;
    private ShopView m_View;

    public ShopPresenter(Shop shop, ShopView shopView){
        m_Shop = shop;
        m_View = shopView;
        InitializeView();
    }

    void InitializeView(){
        m_View.OnShopOrderChanged += SetShopOrder;
        m_View.Init();
        SetTotalPrice();
    }

    void SetShopOrder(){
        m_Shop.SetShopOrder(m_View.GetShopOrder());
        SetTotalPrice();
    }

    void SetTotalPrice(){
        m_View.SetTotalPrice(m_Shop.TotalPrice);
        m_View.CanYouBuy(GameManager.Instance.Money >= m_Shop.TotalPrice);
    }

    public void ResetShop(){
        m_View.ResetView();
    }
}
