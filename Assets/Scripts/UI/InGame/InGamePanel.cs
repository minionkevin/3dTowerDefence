using System;
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

    private TowerSpawnComponent currTowerSpawnPoint;
    private bool shouldCheckInput;

    public override void Init()
    {
        BackBtn.onClick.AddListener(() => {
            UIManager.Instance.HidePanel<InGamePanel>();
            SceneManager.LoadScene("BeginScene");
        });
        
        TowerButtonContainer.gameObject.SetActive(false);
        HealthProgress.value = HealthProgress.maxValue;
        shouldCheckInput = false;
    }

    public void UpdateHealth(int currHealth, int maxHealth)
    {
        HealthLabel.text = currHealth + " / " + maxHealth;
        HealthProgress.value = (float)currHealth / maxHealth * 100;
    }

    public void UpdateCoinLabel(int currCoin)
    {
        CoinLabel.text = "$: " + currCoin;
    }

    public void UpdateRoundLabel(int currRound, int maxRound)
    {
        RoundLabel.text = currRound + " / " + maxRound;
    }

    private void ToggleTowerBtn(bool value)
    {
        TowerButtonContainer.gameObject.SetActive(value);
    }

    public void UpdateTowerUI(TowerSpawnComponent tower)
    {
        if (tower == null)
        {
            shouldCheckInput = false;
            ToggleTowerBtn(false);
            return;
        }
        
        currTowerSpawnPoint = tower;
        ToggleTowerBtn(true);
        shouldCheckInput = true;

        if (tower.CurrTowerInfo == null)
        {
            for (int i = 0; i < TowerButtonList.Count; i++)
            {
                TowerButtonList[i].gameObject.SetActive(true);
                TowerButtonList[i].InitInfo(currTowerSpawnPoint.towerId[i]);
            }
        }
        else
        {
            for (int i = 0; i < TowerButtonList.Count; i++)
            {
                TowerButtonList[i].gameObject.SetActive(false);
            }
            TowerButtonList[1].InitInfo(currTowerSpawnPoint.CurrTowerInfo.nextLevel);
            TowerButtonList[1].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(!shouldCheckInput) return;
        if (currTowerSpawnPoint.CurrTowerInfo == null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currTowerSpawnPoint.HandleCreateTower(currTowerSpawnPoint.towerId[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currTowerSpawnPoint.HandleCreateTower(currTowerSpawnPoint.towerId[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currTowerSpawnPoint.HandleCreateTower(currTowerSpawnPoint.towerId[2]);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currTowerSpawnPoint.HandleCreateTower(currTowerSpawnPoint.CurrTowerInfo.nextLevel);
            }
        }
    }

}
