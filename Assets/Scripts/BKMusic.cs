using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    
    public static BKMusic Instance => instance;
    public AudioSource BkAudioSource;
    
    private void Awake()
    {
        instance = this;

        var musicData = GameDataMgr.Instance.MusicData;
        ToggleMusic(musicData.IsMusicOpen);
        ChangeValue(musicData.musicValue);
    }

    public void ToggleMusic(bool shouldOpen)
    {
        BkAudioSource.mute = !shouldOpen;
    }

    public void ChangeValue(float value)
    {
        BkAudioSource.volume = value;
    }
}