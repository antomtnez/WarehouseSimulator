using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxView : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemAmountText;

    public void SetItemIcon(Item item){
        itemIcon.sprite = item.Icon;
    }

    public void SetItemText(int amount, int maxAmount){
        itemAmountText.SetText($"{amount}/{maxAmount}");
    }
}
