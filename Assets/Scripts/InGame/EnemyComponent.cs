using UnityEngine;
using UnityEngine.AI;

public class EnemyComponent : MonoBehaviour
{
    public Animator Animator;
    public NavMeshAgent agent;

    public bool isDead = false;

    private EnemyInfo enemyInfo;
    private int currHealth;
    private float preAttack;
    
    public void Init(EnemyInfo info)
    {
        enemyInfo = info;
        currHealth = info.health;
        agent.speed = agent.acceleration = info.speed;
        agent.angularSpeed = info.rotateSpeed;
        
        agent.SetDestination(PlayerBaseComponent.Instance.transform.position);
    }

    public void UnderAttack(int value)
    {
        currHealth -= value;
        Animator.SetTrigger("Wound");
        
        if (currHealth <= 0)
        {
            currHealth = 0;
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        isDead = true;
        agent.isStopped = true;
        Animator.SetBool("Dead",true);
    }

    public void HandleDeathAnimation()
    {
        GameLevelMgr.Instance.UpdateEnemyCount(-1);
        Destroy(this);
        if (!GameLevelMgr.Instance.CheckGameOver()) return;
        var panel = UIManager.Instance.ShowPanel<GameOverPanel>();
        panel.InitInfo(GameLevelMgr.Instance.CurrPlayer.Coin,true);
    }

    public void HandleReborn()
    {
        agent.SetDestination(PlayerBaseComponent.Instance.transform.position);
        Animator.SetBool("Run",true);
    }

    private void Update()
    {
        if (isDead) return;
        
        Animator.SetBool("Run",agent.velocity!=Vector3.zero);
        if (Vector3.Distance(transform.position, PlayerBaseComponent.Instance.transform.position) < 3 && Time.time - preAttack >= enemyInfo.attackOffset)
        {
            preAttack = Time.time;
            Animator.SetTrigger("Attack");
        }
    }

    public void HandleAttackAnimation()
    {
        var colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1, 1 << LayerMask.NameToLayer("PlayerBase"));
        foreach (var playerBase in colliders)
        {
            if (playerBase.gameObject == PlayerBaseComponent.Instance.gameObject)
            {
                PlayerBaseComponent.Instance.HandleUnderAttack(enemyInfo.attack);
            }
        }
    }
}
