using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MGBeerManager : MonoBehaviour, IMiniGame
{
    [SerializeField] private RectTransform marker;
    [SerializeField] private RectTransform beerTransform;
    [SerializeField] private Image beerPicture;

    [SerializeField, Range(0f, 10f)] private float rotationSpeed;
    private Vector3 _rotationTarget = new Vector3(0, 0, 89f);
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> _tweener;

    private float _limit;
    [SerializeField] private float maxLimit;

    [SerializeField] private InputActionReference touchInputActionReference;
    private InputAction TouchInputAction => touchInputActionReference.action;

    [SerializeField] private Image rightLimitImage;
    [SerializeField] private Image leftLimitImage;

    private int _goal;
    private int Goal {
        get => _goal;
        set {
            if(value <= 1) MiniGameSuccess?.Invoke(this, new MiniGameEventArgs(new Bike()));
            _goal = value;
        }
    }
    private void Awake() {
        _limit = Random.Range(30f, maxLimit);
        Goal = Random.Range(2, 6);
        SetPicture();
        TouchInputAction.Enable();
        TouchInputAction.started += context => {
            if (!CheckRotation()) return;
            Progress();
        };
        Rotate(out TweenerCore<Quaternion, Vector3, QuaternionOptions> tweener);
        _tweener = tweener;
    }

    private void Progress() {
        Goal--;
        Vector3 target = new Vector3(0, 0, beerTransform.eulerAngles.z + 110f / Goal);
        beerTransform.DORotate(target, .5f);
        DOTween.To(() => beerPicture.fillAmount, f => beerPicture.fillAmount = f,
            beerPicture.fillAmount - beerPicture.fillAmount / Goal, .5f);
    }

    private void SetPicture() {
        rightLimitImage.fillAmount = (_limit + 90f) / 180;
        leftLimitImage.fillAmount = (-_limit + 90f) / 180;
    }

    private bool CheckRotation() {
        return marker.eulerAngles.z < _limit && marker.eulerAngles.z > -_limit;
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
