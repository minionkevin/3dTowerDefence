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
        // todo show visual
        var colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));
        foreach (var monster in colliders)
        {
            // todo add attack
        }
    }

    public void ShootEvent()
    {
        // todo spawn ammo
        var hits = Physics.RaycastAll(new Ray(GunShooter.position, GunShooter.forward), 1000,LayerMask.NameToLayer("Monster"));
        foreach (var monster in hits)
        {
            // todo add attack
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
