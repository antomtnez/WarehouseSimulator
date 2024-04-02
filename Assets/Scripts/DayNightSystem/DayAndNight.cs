using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [Header("Gradients")]
    [SerializeField] Gradient fogGradient;
    [SerializeField] Gradient ambientGradient;
    [SerializeField] Gradient directionalLightGradient;

    [Header("Enviromental Assets")]
    [SerializeField] Light directionalLight;

    [Header("Day Variables")]
    [SerializeField] int m_Day = 1;
    [SerializeField] float m_DayDurationInSeconds;
    [SerializeField] float m_CurrentTime = 0f;
    public int Day => m_Day;
    public float DayDuration => m_DayDurationInSeconds;
    public float CurrentTime => m_CurrentTime;
    private DayTimePresenter m_DayTimePresenter;

    void Start(){
        m_DayTimePresenter = new DayTimePresenter(this, GameObject.FindObjectOfType<DayTimeView>());
    }

    void Update(){
        UpdateTime();
        UpdateDayAndNightCycle();
        if(IsDayFinished())
            FinishDay();
    }

    void UpdateTime(){
        m_CurrentTime += Time.deltaTime / m_DayDurationInSeconds;
        m_DayTimePresenter.SetTime();
    }

    void UpdateDayAndNightCycle(){
        float sunPosition = Mathf.Repeat(m_CurrentTime, 1f);
        directionalLight.transform.rotation = Quaternion.Euler(sunPosition * 180f, 0f, 0f);

        RenderSettings.fogColor = fogGradient.Evaluate(m_CurrentTime);
        RenderSettings.ambientLight = ambientGradient.Evaluate(m_CurrentTime);
        directionalLight.color = directionalLightGradient.Evaluate(m_CurrentTime);
    }

    bool IsDayFinished(){
        return m_CurrentTime >= 1;
    }

    void FinishDay(){
        
    }

    void StartNewDay(){
        m_CurrentTime = 0;
        m_Day++;
        m_DayTimePresenter.SetDay();
    }
}
