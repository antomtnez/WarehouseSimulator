using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Level;
    public List<int> NextLevelRoad;
    public int CurrentExperience;
    public int Money;

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
