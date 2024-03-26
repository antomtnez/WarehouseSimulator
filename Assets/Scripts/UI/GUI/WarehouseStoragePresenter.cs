public class WarehouseStoragePresenter
{
    private WarehouseStorage warehouseStorage;
    private WarehouseStorageView view;

    public WarehouseStoragePresenter(WarehouseStorageView view){
        warehouseStorage = WarehouseStorage.Instance;
        this.view = view;
        InitializeView();
    }

    void InitializeView(){
        view.SetItemCounters(warehouseStorage.ItemStorages);
    }

    public void OnItemStockChanged(ItemStorage itemStorage){
        view.UpdateItemCounter(itemStorage);
    }
}
