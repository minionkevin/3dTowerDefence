using UnityEngine.UI;

public class SettingsPanel : BasePanel
{
    public Toggle MusicToggle;
    public Toggle SfxToggle;
    public Slider MusicSlider;
    public Slider SfxSlider;
    
    private BKMusic musicControl;
    private GameDataMgr gameData;
    public override void Init()
    {
        musicControl = BKMusic.Instance;
        gameData = GameDataMgr.Instance;
        
        var musicData = GameDataMgr.Instance.MusicData;
        MusicToggle.isOn = musicData.IsMusicOpen;
        SfxToggle.isOn = musicData.IsSfxOpen;
        MusicSlider.value = musicData.musicValue;
        SfxSlider.value = musicData.sfxValue;

        MusicToggle.onValueChanged.AddListener ((value) => {
            musicControl.ToggleMusic(value);
            gameData.MusicData.IsMusicOpen = value;
        });
        
        SfxToggle.onValueChanged.AddListener((value) => {
            gameData.MusicData.IsSfxOpen = value;
        });
        
        MusicSlider.onValueChanged.AddListener((value) => {
            musicControl.ChangeValue(value);
            gameData.MusicData.musicValue = value;
        });
        
        SfxSlider.onValueChanged.AddListener((value) => {
            gameData.MusicData.sfxValue = value;
        });
        
    }

    public void HandleClose()
    {
        UIManager.Instance.HidePanel<SettingsPanel>();
        GameDataMgr.Instance.SaveMusicData();
    }
    
}
