using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnComponent : MonoBehaviour
{
    public int MaxWave;
    public int EnemyPerWave;
    public List<int> EnemyId;
    public float SpawnTimeOffset;
    public float DelayTime;
    public float FirstSpawnTime;
    
    private int currEnemyCount;
    private int currEnemyId;

    private void Start()
    {
        Invoke("CreateEnemyWave",FirstSpawnTime);
        GameLevelMgr.Instance.AddEnemySpawnPoint(this);
        GameLevelMgr.Instance.UpdateUI(MaxWave);
    }

    private void CreateEnemyWave()
    {
        currEnemyId = EnemyId[Random.Range(0, EnemyId.Count)];
        currEnemyCount = EnemyPerWave;

        SpawnEnemy();

        MaxWave--;
        GameLevelMgr.Instance.UpdateCurrWave(1);
    }

    private void SpawnEnemy()
    {
        var info = GameDataMgr.Instance.EnemyInfoList[currEnemyId - 1];
        var enemy = Instantiate(Resources.Load<GameObject>(info.res), transform.position, Quaternion.identity).GetComponent<EnemyComponent>();
        enemy.Init(info);
        GameLevelMgr.Instance.UpdateEnemyCount(1);

        currEnemyCount--;
        if (currEnemyCount == 0)
        {
            if (MaxWave > 0)
            {
                Invoke("CreateEnemyWave",DelayTime);   
            }
        }
        else
        {
            Invoke("SpawnEnemy",SpawnTimeOffset);
        }
    }
    
    public bool CheckOver()
    {
        return currEnemyCount == 0 && MaxWave == 0;
    }
}
