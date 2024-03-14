using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VibrationToggle : MonoBehaviour
{

    [SerializeField] private RectTransform handleTransform;
    private Vector2 _startAnchorMax;
    private Vector2 _endAnchorMin = new Vector2(0.5f, 0);
    [SerializeField, Range(0, 10)] private float duration;

    [SerializeField] private Image background;
    [SerializeField] private Color colorOn, colorOff;

    private bool isOn = true;

    private void Start() {
        _startAnchorMax = handleTransform.anchorMax;
    }

    public void OnValueChanged() {
        isOn = !isOn;
        if (isOn) {
            handleTransform.DOAnchorMin(Vector2.zero, duration).SetEase(Ease.OutBack);
            handleTransform.DOAnchorMax(_startAnchorMax, duration).SetEase(Ease.OutBack);
            background.DOColor(colorOn, duration);
        }
        else {
            handleTransform.DOAnchorMin(_endAnchorMin, duration).SetEase(Ease.OutBack);
            handleTransform.DOAnchorMax(Vector2.one, duration).SetEase(Ease.OutBack);
            background.DOColor(colorOff, duration);
        }
    }
    
    public void OnValueChanged(bool IsOn) {
        isOn = IsOn;
        if (isOn) {
            handleTransform.DOAnchorMin(Vector2.zero, duration).SetEase(Ease.OutBack);
            handleTransform.DOAnchorMax(_startAnchorMax, duration).SetEase(Ease.OutBack);
            background.DOColor(colorOn, duration);
        }
        else {
            handleTransform.DOAnchorMin(_endAnchorMin, duration).SetEase(Ease.OutBack);
            handleTransform.DOAnchorMax(Vector2.one, duration).SetEase(Ease.OutBack);
            background.DOColor(colorOff, duration);
        }
    }
}
