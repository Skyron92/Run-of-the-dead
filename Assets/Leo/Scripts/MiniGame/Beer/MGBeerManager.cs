using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    [SerializeField] private Image rightLimitImage;
    [SerializeField] private Image leftLimitImage;
    private void Awake() {
        _limit = Random.Range(30f, maxLimit);
        Debug.Log(_limit);
        SetPicture();
        TouchInputAction.Enable();
        TouchInputAction.started += context => {
            Debug.Log(CheckRotation());
            if (!CheckRotation()) return;
            Debug.Log("Glouglou");
            MiniGameSuccess?.Invoke(this, new MiniGameEventArgs());
        };
        Rotate(out TweenerCore<Quaternion, Vector3, QuaternionOptions> tweener);
        _tweener = tweener;
    }

    private void SetPicture() {
        rightLimitImage.fillAmount = (_limit + 90f) / 180;
        leftLimitImage.fillAmount = (-_limit + 90f) / 180;
    }

    private void Update() {
        Debug.Log(CheckRotation());
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
