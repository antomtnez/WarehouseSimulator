using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopUpView : MonoBehaviour{
    public interface IUIInfoContent{
        string GetName();
        string GetData();
        Object GetContent();
    }
    
    [SerializeField] GameObject m_InfoPopUp;
    [SerializeField] TextMeshProUGUI m_nameText;
    [SerializeField] TextMeshProUGUI m_infoText;
    [Header("Forklift UI")]
    [SerializeField] GameObject m_forkliftInfoPanel;
    [SerializeField] Slider m_forkliftLoadSlider;
    [Header("Rack UI")]
    [SerializeField] GameObject m_rackInfoPanel;
    [SerializeField] Image m_itemIcon;
    private Object m_ObjToShowInfo;

    public void SetUIInfoContent(IUIInfoContent infoContent){
        m_InfoPopUp.SetActive(true);
        m_nameText.SetText(infoContent.GetName());
        m_infoText.SetText(infoContent.GetData());
        m_ObjToShowInfo = infoContent.GetContent();

        if(m_ObjToShowInfo is Forklift){
            SetForkliftUI((Forklift)m_ObjToShowInfo);
            var obj = m_ObjToShowInfo as Carrier;
            obj.OnStateChanged += UpdateForkliftUI;
        }

        if(m_ObjToShowInfo is ItemRack)
            SetItemRackUI((ItemRack)m_ObjToShowInfo);
    }

    void SetForkliftUI(Forklift forklift){
        if(m_rackInfoPanel.activeInHierarchy) m_rackInfoPanel.SetActive(false);
        if(!m_forkliftInfoPanel.activeInHierarchy) m_forkliftInfoPanel.SetActive(true);
        m_forkliftLoadSlider.maxValue = forklift.ItemPileTransporting.ItemMaxStock;
        m_forkliftLoadSlider.value = forklift.ItemPileTransporting.ItemStock;
    }

    void SetItemRackUI(ItemRack itemRack){
        if(m_forkliftInfoPanel.activeInHierarchy) m_forkliftInfoPanel.SetActive(false);
        if(!m_rackInfoPanel.activeInHierarchy) m_rackInfoPanel.SetActive(true);
        m_itemIcon.sprite = WarehouseStorage.Instance.ItemDB.GetItem(itemRack.ItemId).Icon;
    }

    void UpdateForkliftUI(){
        IUIInfoContent iObj = m_ObjToShowInfo as IUIInfoContent;
        m_infoText.SetText(iObj.GetData());
        
        Forklift forklift = m_ObjToShowInfo as Forklift;
        m_forkliftLoadSlider.maxValue = forklift.ItemPileTransporting.ItemMaxStock;
        m_forkliftLoadSlider.value = forklift.ItemPileTransporting.ItemStock;
    }

    public void CloseUIInfoContent(){
        m_forkliftInfoPanel.SetActive(false);
        m_rackInfoPanel.SetActive(false);
        m_InfoPopUp.SetActive(false);

        if(m_ObjToShowInfo != null){
            var obj = m_ObjToShowInfo as Carrier;
            obj.OnStateChanged -= UpdateForkliftUI;
        }
    }
}
