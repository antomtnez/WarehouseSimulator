using UnityEngine;

[CreateAssetMenu(fileName = "ResourceItem", menuName = "Tutorial/Resource Item")]
public class Item : ScriptableObject
{
    public string Id;
    public string Name;
    public Sprite Icon;
    public GameObject ItemGameObject;
    public int MaxStockableInAPile;
}
