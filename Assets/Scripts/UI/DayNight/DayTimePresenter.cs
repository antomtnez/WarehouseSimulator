public class DayTimePresenter{
    private DayAndNight m_DayAndNightSystem;
    private DayTimeView m_DayTimeView;

    public DayTimePresenter(DayAndNight dayAndNight, DayTimeView view){
        m_DayAndNightSystem = dayAndNight;
        m_DayTimeView = view;
        InitializeView();
    }

    void InitializeView(){
        SetDay();
        m_DayTimeView.SetMaxTimeSlider(1);
    }

    public void SetDay(){
        m_DayTimeView.SetDayText(m_DayAndNightSystem.Day);
    }

    public void SetTime(){
        m_DayTimeView.SetTimeSlider(m_DayAndNightSystem.CurrentTime);
    }
}
