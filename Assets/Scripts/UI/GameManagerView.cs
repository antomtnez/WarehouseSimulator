using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] Slider ExperienceSlider;
    [SerializeField] TextMeshProUGUI ExperienceText;
    [SerializeField] TextMeshProUGUI MoneyText;

    public void SetLevel(int level){
        LevelText.SetText($"{level}");
    }

    public void SetMaxExperience(int maxExperience){
        ExperienceSlider.maxValue = maxExperience;
        ExperienceText.SetText($"0/{ExperienceSlider.maxValue}");
    }

    public void SetExperience(int exp){
        ExperienceSlider.value = exp;
        ExperienceText.SetText($"{exp}/{ExperienceSlider.maxValue}");
    }

    public void SetMoney(int amount){
        MoneyText.SetText($"{amount}");
    }
}
