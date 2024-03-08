using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MGBeerManager : MonoBehaviour, IMiniGame
{
    [SerializeField] private RectTransform marker;

    [SerializeField, Range(0f, 10f)] private float rotationSpeed;
    private Vector3 _rotationTarget = new Vector3(0, 0, 89f);
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> _tweener;

    private float _limit;
    [SerializeField] private float maxLimit;

    [SerializeField] private InputActionReference touchInputActionReference;
    private InputAction TouchInputAction => touchInputActionReference.action;
    private void Awake() {
        _limit = Random.Range(-maxLimit, maxLimit);
        TouchInputAction.Enable();
        TouchInputAction.started += context => {
            if (!CheckRotation()) return;
            Debug.Log("Glouglou");
            MiniGameSuccess?.Invoke(this, new MiniGameEventArgs());
        };
        Rotate(out TweenerCore<Quaternion, Vector3, QuaternionOptions> tweener);
        _tweener = tweener;
    }

    private bool CheckRotation() {
        return marker.rotation.z < _limit && marker.rotation.z > -_limit;
    }

    private void Rotate(out TweenerCore<Quaternion, Vector3, QuaternionOptions> tweener) {
        tweener = marker.DORotate(_rotationTarget, rotationSpeed);
        tweener.onComplete += () => marker.DORotate(-_rotationTarget, rotationSpeed).onComplete += () => Rotate();
    }
    
    private void Rotate() {
        marker.DORotate(_rotationTarget, rotationSpeed).onComplete += () => 
            marker.DORotate(-_rotationTarget, rotationSpeed).onComplete += () => Rotate();
    }


    public event IMiniGame.MiniGameSuccessEvent MiniGameSuccess;
}
