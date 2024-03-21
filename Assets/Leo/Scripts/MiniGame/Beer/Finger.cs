using DG.Tweening;
using UnityEngine;

public class Finger : MonoBehaviour
{
    
    [SerializeField] private MovementType movementType;
    private RectTransform _rectTransform;

    [Header("Vertical settings")] 
    [SerializeField] private float upTarget;
    [SerializeField] private float downTarget;
    [SerializeField, Range(0,1)] private float verticalSpeed;

    [Header("Follow settings")] [SerializeField]
    private Transform target;
    private void Start() {
        _rectTransform = GetComponent<RectTransform>();
        switch (movementType) {
            case MovementType.Vertical :
                VerticalMove();
                break;
            case MovementType.FollowTarget :
                Follow(target);
                break;
            default:
                return;
        }
    }

    private void VerticalMove() {
        _rectTransform.DOMoveY(upTarget, verticalSpeed).onComplete += () => _rectTransform.DOMoveY(downTarget, verticalSpeed).onComplete += VerticalMove;
    }

    private void Follow(Transform _target) {
        _rectTransform.DOMove(_target.position, 1.2f).onComplete += () => Follow(_target);
    }
}

public enum MovementType
{
    Vertical,
    FollowTarget,
}