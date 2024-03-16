using DG.Tweening;
using UnityEngine;

public class UIScaleAnimationProvider : MonoBehaviour
{
    
    private void Awake() => ScaleAnimation();

    private void ScaleAnimation() => 
        transform.DOScale(Vector3.one * 0.5f, 1f).onComplete += () => 
            transform.DOScale(Vector3.one, 1f).onComplete += () => ScaleAnimation();
    
}
