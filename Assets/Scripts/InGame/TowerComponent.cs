using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerComponent : MonoBehaviour
{
    public Transform Head;
    public Transform Shooter;

    private float rotateSpeed = 20f;
    private TowerInfo info;
    private EnemyComponent targetEnemy;
    private List<EnemyComponent> targetEnemyList = new List<EnemyComponent>();
    private Vector3 targetEnemyPos;
    private float currAtkTime;
    
    public void Init(TowerInfo info)
    {
        this.info = info;
    }

    void Update()
    {
        if (info.atkType == 1)
        {
            if (targetEnemy == null || targetEnemy.isDead || Vector3.Distance(transform.position, targetEnemy.transform.position) > info.atkRange)
            {
                targetEnemy = GameLevelMgr.Instance.FindNewEnemy(transform.position, info.atkRange);
            }
            if (targetEnemy == null) return;
            
            targetEnemyPos = targetEnemy.transform.position;
            targetEnemyPos.y = Head.position.y;

            Head.rotation = Quaternion.Slerp(Head.rotation, Quaternion.LookRotation(targetEnemyPos - Head.position), rotateSpeed * Time.deltaTime);

            if (Vector3.Angle(Head.forward, targetEnemyPos - Head.position) <= 50 && Time.time - currAtkTime >= info.atkOffset)
            {
                targetEnemy.UnderAttack(info.attack);
                currAtkTime = Time.time;
            }
        }
        else
        {
            var targetEnemyList = GameLevelMgr.Instance.FindNewEnemies(transform.position, info.atkRange);
            if (targetEnemyList.Count <= 0) return;
            if (Time.time - currAtkTime >= info.atkOffset)
            {
                foreach (var enemy in targetEnemyList)
                {
                    enemy.UnderAttack(info.attack);
                }
                currAtkTime = Time.time;
            }
        }
    }
}
