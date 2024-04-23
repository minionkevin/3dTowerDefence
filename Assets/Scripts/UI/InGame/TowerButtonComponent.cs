using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerButtonComponent : MonoBehaviour
{
    public TextMeshProUGUI CostLabel;
    public TextMeshProUGUI NameLabel;
    public Image TowerIcon;

    public void InitInfo(int id)
    {
        var towerInfo = GameDataMgr.Instance.TowerInfoList[id - 1];
        TowerIcon.sprite = Resources.Load<Sprite>(towerInfo.imgRes);
        NameLabel.text = towerInfo.name;

        if (towerInfo.cost > GameLevelMgr.Instance.CurrPlayer.Coin)
        {
            CostLabel.text = "NOT ENOUGH";
        }
        else
        {
            CostLabel.text = "$" + towerInfo.cost;
        }
    }
}
