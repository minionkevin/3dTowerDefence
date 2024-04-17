using System;
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
        // Destroy(this);
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
        if (Vector3.Distance(transform.position, PlayerBaseComponent.Instance.transform.position) < 5 && Time.time - preAttack>= enemyInfo.attackOffset)
        {
            preAttack = Time.time;
            Animator.SetTrigger("attack");
        }
    }

    public void HandleAttackAnimation()
    {
        
    }
}
