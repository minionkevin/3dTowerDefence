using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;

    public PlayerComponent CurrPlayer;
    private List<EnemySpawnComponent> enemySpawnList = new List<EnemySpawnComponent>();
    private List<EnemyComponent> enemyList = new List<EnemyComponent>();
    private int currWave;
    private int maxWave;

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
        return enemyList.Count <= 0;
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

    public void AddEnemy(EnemyComponent newEnemy)
    {
        enemyList.Add(newEnemy);
    }
    
    public void RemoveEnemy(EnemyComponent newEnemy)
    {
        enemyList.Remove(newEnemy);
    }
    
    public void HandleSuccess()
    {
        
    }

    public EnemyComponent FindNewEnemy(Vector3 pos, int range)
    {
        foreach (var enemy in enemyList)
        {
            if (!enemy.isDead && Vector3.Distance(pos, enemy.transform.position) <= range)
            {
                return enemy;
            }
        }
        return null;
    }
    
    public List<EnemyComponent> FindNewEnemies(Vector3 pos, int range)
    {
        List<EnemyComponent> targets = new List<EnemyComponent>();
        foreach (var enemy in enemyList)
        {
            if (!enemy.isDead && Vector3.Distance(pos, enemy.transform.position) <= range)
            {
                targets.Add(enemy);
            }
        }
        return targets;
    }

    public void CleanUp()
    {
        enemySpawnList.Clear();
        enemyList.Clear();
        currWave = maxWave = 0;
        CurrPlayer = null;
    }
}
