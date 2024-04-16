using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGamePanel : BasePanel
{
    public TextMeshProUGUI HealthLabel;
    public Slider HealthProgress;
    public TextMeshProUGUI CoinLabel;
    public TextMeshProUGUI RoundLabel;

    public Button BackBtn;
    public List<TowerButtonComponent> TowerButtonList;
    public RectTransform TowerButtonContainer;

    public override void Init()
    {
        BackBtn.onClick.AddListener(() => {
            UIManager.Instance.HidePanel<InGamePanel>();
            SceneManager.LoadScene("BeginScene");
        });
        
        TowerButtonContainer.gameObject.SetActive(false);
    }

    public void UpdateHealth(int currHealth, int maxHealth)
    {
        HealthLabel.text = currHealth + " / " + maxHealth;
        HealthProgress.value = (float)currHealth / maxHealth;
    }

    public void UpdateCoinLabel(int currCoin)
    {
        CoinLabel.text = "$: " + currCoin;
    }

    public void UpdateRoundLabel(int currRound, int maxRound)
    {
        RoundLabel.text = currRound + " / " + maxRound;
    }

}
