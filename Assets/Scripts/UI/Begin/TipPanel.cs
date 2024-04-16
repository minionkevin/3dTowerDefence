using TMPro;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public TextMeshProUGUI DescriptionLabel;
    public Button ConfirmBtn;
    
    
    public override void Init()
    {
        ConfirmBtn.onClick.AddListener(() => {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    public void ChangeDescription(string value)
    {
        DescriptionLabel.text = value;
    }
}
