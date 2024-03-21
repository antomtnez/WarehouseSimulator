using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Items/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> ResourceTypes = new List<Item>();
    private Dictionary<string, Item> m_Database;
    
    public void Init()
    {
        m_Database = new Dictionary<string, Item>();
        foreach (var resourceItem in ResourceTypes)
        {
            m_Database.Add(resourceItem.Id, resourceItem);
        }
    }

    public Item GetItem(string uniqueId)
    {
        m_Database.TryGetValue(uniqueId, out Item type);
        return type;
    }
}