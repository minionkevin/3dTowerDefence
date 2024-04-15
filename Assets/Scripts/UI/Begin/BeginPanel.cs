using UnityEngine;

public class BeginPanel : BasePanel
{
    public override void Init()
    {
        
    }

    public void HandleStartClick()
    {
        Camera.main.GetComponent<CameraControl>().TurnLeft(() => {
            UIManager.Instance.ShowPanel<CharacterSelectionPanel>();
        });
        UIManager.Instance.HidePanel<BeginPanel>();
    }

    public void HandleSettingClick()
    {
        UIManager.Instance.ShowPanel<SettingsPanel>();
    }

    public void HandleAboutClick()
    {
        
    }

    public void HandleQuit()
    {
        Application.Quit();
    }
}
