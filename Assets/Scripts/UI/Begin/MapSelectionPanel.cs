using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelectionPanel : BasePanel
{
    public Image MapIcon;
    public TextMeshProUGUI NameLabel;
    public TextMeshProUGUI DescriptionLabel;

    public Button StartBtn;
    public Button BackBtn;
    public Button LeftBtn;
    public Button RightBtn;
    
    private MapInfo currMapInfo;
    private int currIndex;
    private int maxIndex;
    
    public override void Init()
    {
        maxIndex = GameDataMgr.Instance.MapInfoList.Count-1;
        HandleMapChange();
        
        StartBtn.onClick.AddListener(()=>{
            UIManager.Instance.HidePanel<MapSelectionPanel>();
            var load = SceneManager.LoadSceneAsync(currMapInfo.sceneName);
            load.completed += (obj) => {
                GameLevelMgr.Instance.InitInfo(currMapInfo);
            };
        });
        
        BackBtn.onClick.AddListener(() => {
            UIManager.Instance.HidePanel<MapSelectionPanel>();
            UIManager.Instance.ShowPanel<CharacterSelectionPanel>();
        });
        
        RightBtn.onClick.AddListener(() => {
            ++currIndex;
            if (currIndex > maxIndex) currIndex = 0;
            HandleMapChange();
        });

        LeftBtn.onClick.AddListener(() => {
            --currIndex;
            if (currIndex < 0) currIndex = maxIndex;
            HandleMapChange();
        });
    }

    private void HandleMapChange()
    {
        currMapInfo = GameDataMgr.Instance.MapInfoList[currIndex];
        MapIcon.sprite = Resources.Load<Sprite>(currMapInfo.imgRes);
        NameLabel.text = currMapInfo.name;
        DescriptionLabel.text = currMapInfo.tips;
    }

    
}