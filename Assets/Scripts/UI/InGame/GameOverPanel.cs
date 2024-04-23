using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Button ConfirmBtn;
    public TextMeshProUGUI GameStateLabel;
    public TextMeshProUGUI CoinLabel;

    public override void Init()
    {
        ConfirmBtn.onClick.AddListener(() => {
            UIManager.Instance.HidePanel<GameOverPanel>();
            UIManager.Instance.HidePanel<InGamePanel>();

            GameLevelMgr.Instance.CleanUp();
            SceneManager.LoadScene("BeginScene");
        });
    }

    public void InitInfo(int coin, bool isSuccess)
    {
        CoinLabel.text = "Get $" + coin + " from this try";
        GameStateLabel.text = isSuccess ? "GAME SUCCESS": "GAME FAIL";

        GameDataMgr.Instance.PlayerData.Coin += coin;
        GameDataMgr.Instance.SavePlayerData();
    }
}
