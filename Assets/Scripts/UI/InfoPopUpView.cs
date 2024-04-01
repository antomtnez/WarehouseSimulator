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

    public void SetUIInfoContent(IUIInfoContent infoContent){
        m_nameText.SetText(infoContent.GetName());
        m_infoText.SetText(infoContent.GetData());
        var obj = infoContent.GetContent();
        if(obj is Forklift)
            SetForkliftUI((Forklift)obj);

        if(obj is ItemRack)
            SetItemRackUI((ItemRack)obj);
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

    public void CloseUIInfoContent(){
        m_forkliftInfoPanel.SetActive(false);
        m_rackInfoPanel.SetActive(false);
        m_InfoPopUp.SetActive(false);
    }
}
