﻿using DG.Tweening;
using MiniGame.Zombie;
using UnityEngine;

public class Finger : MonoBehaviour
{
    
    [SerializeField] private MovementType movementType;
    private RectTransform _rectTransform;

    [Header("Vertical settings")] 
    private float _upTarget;
    private float _downTarget;
    [SerializeField, Range(0,1)] private float verticalSpeed;

    [Header("Follow settings")] [SerializeField]
    private Transform target; [SerializeField]
    private BirdMovement birdMovement;
    private void OnEnable() {
        _rectTransform = GetComponent<RectTransform>();
        switch (movementType) {
            case MovementType.Vertical :
                _upTarget = transform.position.y;
                _downTarget = transform.position.y - 1000;
                VerticalMove();
                break;
            case MovementType.FollowTarget :
                birdMovement.Moved += () => Follow(target);
                birdMovement.Destroyed += () => birdMovement.Moved -= () => Follow(target);
                break;
            default:
                return;
        }
    }

    private void VerticalMove() {
        _rectTransform.DOMoveY(_downTarget, verticalSpeed).onComplete += () => _rectTransform.DOMoveY(_upTarget, verticalSpeed).onComplete += VerticalMove;
    }

    private void Follow(Transform _target) {
        _rectTransform.DOMove(_target.position, 1f);
    }
}

public enum MovementType
{
    Vertical,
    FollowTarget,
}