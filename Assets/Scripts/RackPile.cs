using UnityEngine;

public class RackPile : MonoBehaviour
{
    public Item Item;
    public GameObject ItemPileGO;

    public void Init(Item item){
        Item = item;
        if(ItemPileGO == null) ItemPileGO = Instantiate(Item.ItemGameObject, transform);
    }

    public void UpdateItemPileGO(bool IsEmpty){
        if(IsEmpty){
            ItemPileGO.SetActive(false);
        }else{
            ItemPileGO.SetActive(true);
        }
    }

    public bool IsEmpty(){
        return ItemPileGO.activeInHierarchy;
    }

}
