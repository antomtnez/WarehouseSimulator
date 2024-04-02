using UnityEngine;

public class InfoPopUpPresenter
{
    private InfoPopUpView view;

    public InfoPopUpPresenter(InfoPopUpView view){
        this.view = view;
        InitializeView();
    }

    void InitializeView(){
        SetInfoPopUpContent(null);
    }

    public void SetInfoPopUpContent(InfoPopUpView.IUIInfoContent infoContent){
        if(infoContent == null){
            view.CloseUIInfoContent();
        }else{
            view.SetUIInfoContent(infoContent);
        }
    }
}
