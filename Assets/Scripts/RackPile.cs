using UnityEngine;

public class RackPile : MonoBehaviour
{
    public Item Item;
    public GameObject ItemPileGO;

    public void Init(Item item){
        Item = item;
        if(ItemPileGO == null) ItemPileGO = Instantiate(Item.ItemGameObject, transform);
    }

    public void ActivePileStockGO(bool hasStock){
        if(hasStock){
            ItemPileGO.SetActive(true);
        }else{
            ItemPileGO.SetActive(false);
        }
    }

    public bool IsEmpty(){
        return ItemPileGO.activeInHierarchy;
    }

}
