using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Items/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> ItemTypes = new List<Item>();
    private Dictionary<string, Item> m_Database;
    
    public void Init()
    {
        m_Database = new Dictionary<string, Item>();
        foreach (var item in ItemTypes)
        {
            m_Database.Add(item.Id, item);
        }
    }

    public Item GetItem(string uniqueId)
    {
        m_Database.TryGetValue(uniqueId, out Item type);
        return type;
    }
}