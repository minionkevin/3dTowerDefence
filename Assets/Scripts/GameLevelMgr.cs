using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;

    public PlayerComponent CurrPlayer;
    private List<EnemySpawnComponent> enemySpawnList = new List<EnemySpawnComponent>();
    private int currWave;
    private int maxWave;
    private int currEnemeyCount;

    private GameLevelMgr()
    {
        
    }

    public void InitInfo(MapInfo mapInfo)
    {
        UIManager.Instance.ShowPanel<InGamePanel>();
        var roleInfo = GameDataMgr.Instance.CurrHeroInfo;

        var spawnTrans = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        var playerObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), spawnTrans.position, spawnTrans.rotation);
        CurrPlayer = playerObj.GetComponent<PlayerComponent>();
        CurrPlayer.InitPlayer(roleInfo.attack,mapInfo.coin);
        
        Camera.main.GetComponent<CameraFollowComponent>().SetupTargetPos(playerObj.transform);
        
        PlayerBaseComponent.Instance.UpdateHealth(mapInfo.baseHealth,mapInfo.baseHealth);
    }

    public void AddEnemySpawnPoint(EnemySpawnComponent spawnPoint)
    {
        enemySpawnList.Add(spawnPoint);
    }

    public bool CheckGameOver()
    {
        foreach (EnemySpawnComponent enemy in enemySpawnList)
        {
            if (!enemy.CheckOver()) return false;
        }
        return currEnemeyCount <= 0;
    }

    public void UpdateUI(int num)
    {
        maxWave += num;
        currWave = maxWave;
        
        UIManager.Instance.GetPanel<InGamePanel>().UpdateRoundLabel(currWave,maxWave);
    }

    public void UpdateCurrWave(int num)
    {
        currWave -= num;
        UIManager.Instance.GetPanel<InGamePanel>().UpdateRoundLabel(currWave,maxWave);
    }

    public void UpdateEnemyCount(int num)
    {
        currEnemeyCount += num;
    }
    
    public void HandleSuccess()
    {
        
    }

    public void CleanUp()
    {
        enemySpawnList.Clear();
        currEnemeyCount = currWave = maxWave = 0;
        CurrPlayer = null;
    }
}
