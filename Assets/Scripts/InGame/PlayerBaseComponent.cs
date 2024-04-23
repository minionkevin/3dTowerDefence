using UnityEngine;

public class PlayerBaseComponent : MonoBehaviour
{
    private int maxHealth;
    private int currHealth;
    private bool isDead = false;

    private static PlayerBaseComponent instance;
    public static PlayerBaseComponent Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealth(int currHp, int maxHp)
    {
        currHealth = currHp;
        maxHealth = maxHp;
        
        UIManager.Instance.GetPanel<InGamePanel>().UpdateHealth(currHealth,maxHp);
    }

    public void HandleUnderAttack(int damage)
    {
        if (isDead) return;
        currHealth -= damage;
        if (currHealth <= 0)
        {
            var panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo(GameLevelMgr.Instance.CurrPlayer.Coin/2,false);
            currHealth = 0;
            isDead = true;
        }
        UpdateHealth(currHealth, maxHealth);

        // TODO game over
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
