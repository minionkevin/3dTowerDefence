using System.Collections.Generic;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    public MusicData MusicData;
    public PlayerData PlayerData;
    public List<RoleInfo> RoleInfoList;
    public List<MapInfo> MapInfoList;
    public List<EnemyInfo> EnemyInfoList;

    public RoleInfo CurrHeroInfo;
    
    private GameDataMgr()
    {
        RoleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        MapInfoList = JsonMgr.Instance.LoadData<List<MapInfo>>("MapInfo");
        EnemyInfoList = JsonMgr.Instance.LoadData<List<EnemyInfo>>("EnemyInfo");
        
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
