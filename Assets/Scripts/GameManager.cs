using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Level;
    public List<int> NextLevelRoad;
    public int CurrentExperience;
    public int Money;
    private GameManagerPresenter m_GameManagerPresenter;

    void Awake(){
        Instance = this;
        m_GameManagerPresenter = new GameManagerPresenter(FindObjectOfType<GameManagerView>());
    }  

    public int GetExperienceToNextLevel(){
        return NextLevelRoad[Level+1];
    }

    public void OrderCompleted(Order order){
        AddExperience(100);
        AddMoney(order.Reward);
        m_GameManagerPresenter.OnPlayerStatsChanged();
    }

    void AddExperience(int exp){
        CurrentExperience += exp;
        CheckToUpgradeLevel();
    }

    void CheckToUpgradeLevel(){
        for(int i=Level; i < NextLevelRoad.Count; i++)
            if(CurrentExperience >= NextLevelRoad[i])
                Level = i;
    }

    void AddMoney(int cash){
        Money += cash;
    }

    public void ExpendMoney(int cash){
        Money -= cash;
        m_GameManagerPresenter.OnPlayerStatsChanged();
    }
}
