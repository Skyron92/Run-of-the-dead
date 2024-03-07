using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides a scale animation to the button it's attached to
/// </summary>
[RequireComponent(typeof(Button))]
public class ButtonScaleAnimationProvider : MonoBehaviour
{
    private Vector3 _startScale;
    
    [HideInInspector] public float maxScale;
    [HideInInspector] public float scaleUpDuration;
    [HideInInspector] public float scaleDownDuration;

    public Sequence sequence;
    
    private void Awake() {
        _startScale = transform.lossyScale;
        GetComponent<Button>().onClick.AddListener(ScaleAnimation);
        Debug.Log(_startScale);
    }

    public void ScaleAnimation() {
        if (sequence != null) {
            sequence.Kill();
            sequence = null;
        }
        sequence = DOTween.Sequence();
        var tween = transform.DOScale(maxScale, scaleUpDuration).onComplete +=
            () => transform.DOScale(_startScale, scaleDownDuration);
        sequence.AppendCallback(tween);
    }
}