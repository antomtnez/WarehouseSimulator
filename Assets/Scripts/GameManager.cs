using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Level;
    public List<int> NextLevelRoad;
    public int CurrentExperience;
    public int Money;
    [SerializeField] List<ItemRack> m_ItemRacks = new List<ItemRack>();
    [SerializeField] ItemDatabase m_ItemDatabase;
    public ItemDatabase ItemDB => m_ItemDatabase;

    void Awake(){
        Instance = this;
        m_ItemDatabase.Init();
    }

    void Start(){
        FindItemRacks();
    }

    void FindItemRacks(){
        foreach(ItemRack itemRack in FindObjectsOfType<ItemRack>())
            m_ItemRacks.Add(itemRack);
    }

    public void AddExperience(int exp){
        CurrentExperience += exp;
        CheckToUpgradeLevel();
    }

    public void AddMoney(int cash){
        Money += cash;
    }

    void CheckToUpgradeLevel(){
        for(int i=Level; i < NextLevelRoad.Count; i++)
            if(CurrentExperience >= NextLevelRoad[i])
                Level = i;
    }
}
