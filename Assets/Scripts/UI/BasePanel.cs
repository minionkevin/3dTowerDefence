using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasController;

    private float alphaSpeed = 0.75f;

    protected virtual void Awake()
    {
        canvasController = GetComponent<CanvasGroup>();
        if(canvasController==null) Debug.LogError("PLEASE ADD CANVAS GROUP TO PANEL");
    }
    
    protected virtual void Start()
    {
        Init();
    }

    public abstract void Init();

    public virtual async void TransitionIn()
    {
        await canvasController.DOFade(1, alphaSpeed).SetEase(Ease.OutBack).AsyncWaitForCompletion();
    }

    public virtual async void TransitionOut()
    {
        await canvasController.DOFade(0, alphaSpeed).SetEase(Ease.InBack).AsyncWaitForCompletion();
    }
}
