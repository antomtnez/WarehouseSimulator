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
    [SerializeField] float m_DayDurationInSeconds = 60f;

    private float m_CurrentTime = 0f;

    void Update(){
        UpdateTime();
        UpdateDayAndNightCycle();
    }

    void UpdateTime(){
        m_CurrentTime += Time.deltaTime / m_DayDurationInSeconds;
        m_CurrentTime = Mathf.Repeat(m_CurrentTime, 1f);
    }

    void UpdateDayAndNightCycle(){
        float sunPosition = Mathf.Repeat(m_CurrentTime + .25f, 1f);
        directionalLight.transform.rotation = Quaternion.Euler(sunPosition * 360f, 0f, 0f);

        RenderSettings.fogColor = fogGradient.Evaluate(m_CurrentTime);
        RenderSettings.ambientLight = ambientGradient.Evaluate(m_CurrentTime);
        directionalLight.color = directionalLightGradient.Evaluate(m_CurrentTime);
    }



}
