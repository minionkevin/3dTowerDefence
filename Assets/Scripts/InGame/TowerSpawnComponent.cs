
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnComponent : MonoBehaviour
{
    public TowerInfo CurrTowerInfo = null;
    private GameObject towerPrefab= null;

    public List<int> towerId;
    
    private void OnTriggerEnter(Collider other)
    {
        if (CurrTowerInfo != null && CurrTowerInfo.nextLevel == 0) return;
        
        var currentPanel = UIManager.Instance.GetPanel<InGamePanel>();
        currentPanel.UpdateTowerUI(this);
    }

    private void OnTriggerExit(Collider other)
    {
        var currentPanel = UIManager.Instance.GetPanel<InGamePanel>();
        currentPanel.UpdateTowerUI(null);
    }
    
    public void HandleCreateTower(int id)
    {
        var info = GameDataMgr.Instance.TowerInfoList[id - 1];
        if (info.cost > GameLevelMgr.Instance.CurrPlayer.Coin) return;
        
        GameLevelMgr.Instance.CurrPlayer.AddCoin(-info.cost);
        if (towerPrefab != null)
        {
            Destroy(towerPrefab);
            towerPrefab = null;
        }
        towerPrefab = Instantiate(Resources.Load<GameObject>(info.res), transform);
        towerPrefab.GetComponent<TowerComponent>().Init(info);
        CurrTowerInfo = info;

        if (CurrTowerInfo.nextLevel != 0)
        {
            UIManager.Instance.GetPanel<InGamePanel>().UpdateTowerUI(this);
        }
    }
}
