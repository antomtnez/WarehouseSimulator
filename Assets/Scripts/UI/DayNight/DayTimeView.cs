using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayTimeView : MonoBehaviour
{
    [SerializeField] Slider m_TimeSlider;
    [SerializeField] TextMeshProUGUI m_DayText;

    public void SetDayText(int dayNumber){
        m_DayText.SetText($"Day {dayNumber}");
    }

    public void SetMaxTimeSlider(float maxTime){
        m_TimeSlider.maxValue = maxTime;
        SetTimeSlider(0);
    }

    public void SetTimeSlider(float time){
        m_TimeSlider.value = time;
    }
}
