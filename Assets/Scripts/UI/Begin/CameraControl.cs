using UnityEngine;
using UnityEngine.Events;

public class CameraControl : MonoBehaviour
{
    public Animator CameraAnimator;

    private UnityAction animationFinish;
    void Start()
    {
        
    }

    public void TurnLeft(UnityAction action)
    {
        CameraAnimator.SetTrigger("Left");
        animationFinish = action;
    }
    
    public void TurnRight(UnityAction action)
    {
        CameraAnimator.SetTrigger("Right");
        animationFinish = action;
    }

    public void HandleAnimationFinish()
    {
        animationFinish?.Invoke();
        animationFinish = null;
    }
}
