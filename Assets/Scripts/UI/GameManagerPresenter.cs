public class GameManagerPresenter
{
    private GameManager gameManager;
    private GameManagerView gameManagerView;

    public GameManagerPresenter(GameManagerView view){
        gameManager = GameManager.Instance;
        gameManagerView = view;
        OnPlayerStatsChanged();
    }

    public void OnPlayerStatsChanged(){
        gameManagerView.SetLevel(gameManager.Level);
        gameManagerView.SetMaxExperience(gameManager.GetExperienceToNextLevel());
        gameManagerView.SetExperience(gameManager.CurrentExperience);
        gameManagerView.SetMoney(gameManager.Money);
    }

}
