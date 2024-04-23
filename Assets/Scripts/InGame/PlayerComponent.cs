using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public Transform GunShooter;
    public int Coin;
    
    private int attack;
    private float rotateSpeed = 200;
    private float moveSpeed = 10;
    private Animator animator;
    
    public void InitPlayer(int atk, int coin)
    {
        attack = atk;
        this.Coin = coin;
        UpdateCoin();
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl)) animator.SetLayerWeight(2,1);
        else if (Input.GetKeyUp(KeyCode.LeftControl)) animator.SetLayerWeight(2,0);
        
        if(Input.GetKeyDown(KeyCode.R)) animator.SetTrigger("Roll");
        if(Input.GetMouseButtonDown(0)) animator.SetTrigger("Fire");
    }

    public void KnifeEvent()
    {
        var colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));
        foreach (var enemy in colliders)
        {
            var enemyComponent = enemy.GetComponent<EnemyComponent>();
            if(enemyComponent.isDead) continue;
            enemyComponent.UnderAttack(attack);
            break;
        }
    }

    public void ShootEvent()
    {
        var hits = Physics.RaycastAll(new Ray(GunShooter.position, GunShooter.forward), 1000,LayerMask.NameToLayer("Monster"));
        foreach (var hit in hits)
        {
            var enemyComponent = hit.collider.gameObject.GetComponent<EnemyComponent>();
            if(enemyComponent.isDead) continue;
            enemyComponent.UnderAttack(attack);
            break;
        }
    }

    private void UpdateCoin()
    {
        UIManager.Instance.GetPanel<InGamePanel>().UpdateCoinLabel(Coin);
    }

    public void AddCoin(int coin)
    {
        this.Coin += coin;
        UpdateCoin();
    }
}
