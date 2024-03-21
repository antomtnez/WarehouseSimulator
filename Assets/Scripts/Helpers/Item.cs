using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public string Id;
    public string Name;
    public Sprite Icon;
    public GameObject ItemGameObject;
    public int MaxStockableInAPile;
    public int BuyingPrice;
    public int SellingPrice;
}
