using UnityEngine;

public interface IStorageInteractable
{
    public string GetItemId();
    public Vector3 GetPosition();
    public bool IsEmpty();
    public int AddItem(string itemId, int amount);
    public int AddItem(int amount);
    public int GetItem(int requiredAmount);
}
