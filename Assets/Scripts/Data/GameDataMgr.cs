using System.Collections.Generic;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    public MusicData MusicData;
    public List<RoleInfo> RoleInfoList;
    public PlayerData PlayerData;
    public RoleInfo CurrHeroInfo;

    private GameDataMgr()
    {
        RoleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        MusicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        PlayerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
    }

    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(MusicData,"MusicData");
    }

    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(PlayerData,"PlayerData");
    }
}
