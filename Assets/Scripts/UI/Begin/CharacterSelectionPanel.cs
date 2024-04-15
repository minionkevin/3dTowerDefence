using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSelectionPanel : BasePanel
{
    public Button LeftBtn;
    public Button RightBtn;

    public Button UnlockBtn;
    public TextMeshProUGUI CoinLabel;
    public TextMeshProUGUI CostLabel;
    public TextMeshProUGUI CharacterNameLabel;

    public Button StartBtn;
    public Button BackBtn;

    private Transform heroSpawnTrans;
    private GameObject currHero;
    private RoleInfo currHeroInfo;
    private int currIndex;
    private int maxIndex;

    private PlayerData playerData;
    
    public override void Init()
    {
        playerData = GameDataMgr.Instance.PlayerData;
        maxIndex = GameDataMgr.Instance.RoleInfoList.Count - 1;
            
        heroSpawnTrans = GameObject.Find("HeroSpawnPos").transform;
        CoinLabel.text = playerData.Coin.ToString();
        CostLabel.rectTransform.localScale = Vector3.one;
        
        HandleHeroChange();
        
        LeftBtn.onClick.AddListener(() => {
            --currIndex;
            if (currIndex < 0) currIndex = maxIndex;
            HandleHeroChange();
        });
        
        RightBtn.onClick.AddListener(() => {
            ++currIndex;
            if (currIndex > maxIndex) currIndex = 0;
            HandleHeroChange();
        });
        
        UnlockBtn.onClick.AddListener(() => {
            if (!CanAfford())
            {
                HandleCantAffordAnimation();
                return;
            }
            playerData.Coin -= currHeroInfo.cost;
            CoinLabel.text = playerData.Coin.ToString();
                
            playerData.UnlockCharacter.Add(currHeroInfo.id);
            GameDataMgr.Instance.SavePlayerData();
            
            HandleBuyAnimation();
        });
        
        StartBtn.onClick.AddListener(() => {
            GameDataMgr.Instance.CurrHeroInfo = currHeroInfo;
            UIManager.Instance.HidePanel<CharacterSelectionPanel>();
        });
        
        BackBtn.onClick.AddListener(() => { 
            UIManager.Instance.HidePanel<CharacterSelectionPanel>();
            Camera.main.GetComponent<CameraControl>().TurnRight(() => {
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
        });
    }

    public override void TransitionOut(UnityAction callBack)
    {
        base.TransitionOut(callBack);
        if (currHero == null) return;
        DestroyImmediate(currHero);
        currHero = null;
    }


    private async void HandleCantAffordAnimation()
    {
        await CostLabel.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.15f).SetEase(Ease.Flash).SetLoops(3, LoopType.Yoyo).AsyncWaitForCompletion();
        CostLabel.rectTransform.localScale = Vector3.one;
    }
    
    private async void HandleBuyAnimation()
    {
        var timeline = DOTween.Sequence();
        timeline.Insert(0,CostLabel.DOColor(Color.blue,0.35f));
        timeline.Insert(0.75f, CostLabel.rectTransform.DOScale(1.25f, 0.5f));
        await timeline.Play().AsyncWaitForCompletion();
        ToggleUnlockBtn(false);
        ToggleStartBtn(true);
    }

    private void HandleHeroChange()
    {
        if (currHero != null)
        {
            Destroy(currHero);
            currHero = null;
        }
        currHeroInfo = GameDataMgr.Instance.RoleInfoList[currIndex];
        var newHero = Instantiate(Resources.Load<GameObject>(currHeroInfo.res), heroSpawnTrans);
        currHero = newHero;
        CharacterNameLabel.text = currHeroInfo.name;

        UpdateUnlockInfo();
        ToggleUnlockBtn(!playerData.UnlockCharacter.Contains(currHeroInfo.id) && currHeroInfo.cost != 0);
        ToggleStartBtn(playerData.UnlockCharacter.Contains(currHeroInfo.id) || currHeroInfo.cost == 0);
    }

    private bool CanAfford()
    {
        return playerData.Coin >= currHeroInfo.cost;
    }

    private void UpdateUnlockInfo()
    {
        CostLabel.color = !CanAfford() ? Color.red : Color.yellow;
    }

    private void ToggleUnlockBtn(bool value)
    {
        UnlockBtn.gameObject.SetActive(value);
        if (!value) return;
        CostLabel.text = currHeroInfo.cost.ToString();
    }

    private void ToggleStartBtn(bool value)
    {
        StartBtn.gameObject.SetActive(value);
    }
}
