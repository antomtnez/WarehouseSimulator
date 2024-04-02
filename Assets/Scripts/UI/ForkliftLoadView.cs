using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForkliftLoadView : MonoBehaviour{
    
    [SerializeField] Button m_addAmountToLoadButton;
    [SerializeField] TextMeshProUGUI m_amountToLoadText;
    [SerializeField] Button m_restAmountToLoadButton;
    private Forklift m_ForkliftControlled;

    public void Init(Forklift forklift){
        m_ForkliftControlled = forklift;
        SetForkliftLoadUIControllerButtons();
        CheckAmountToBlockButtons();
        SetAmountToLoadByForkliftText();
    }
    
    void SetForkliftLoadUIControllerButtons(){
        m_addAmountToLoadButton.onClick.AddListener(()=>{
            m_ForkliftControlled.SetAmountToLoadByForklift(m_ForkliftControlled.AmountRequiredToTransport + 1);
            SetAmountToLoadByForkliftText();
            CheckAmountToBlockButtons();
        });
        m_addAmountToLoadButton.onClick.AddListener(()=>{
            m_ForkliftControlled.SetAmountToLoadByForklift(m_ForkliftControlled.AmountRequiredToTransport - 1);
            SetAmountToLoadByForkliftText();
            CheckAmountToBlockButtons();
        });
    }

    void CheckAmountToBlockButtons(){
        m_restAmountToLoadButton.interactable = m_ForkliftControlled.AmountRequiredToTransport >= 1;
        m_addAmountToLoadButton.interactable = m_ForkliftControlled.AmountRequiredToTransport < m_ForkliftControlled.ItemPileTransporting.ItemMaxStock;
    }

    void SetAmountToLoadByForkliftText(){
        m_amountToLoadText.SetText($"{m_ForkliftControlled.AmountRequiredToTransport}/{m_ForkliftControlled.ItemPileTransporting.ItemMaxStock}");
    }

    public void Reset(){
        m_addAmountToLoadButton.onClick.RemoveAllListeners();
        m_restAmountToLoadButton.onClick.RemoveAllListeners();
    }
}
