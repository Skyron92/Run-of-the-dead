using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class ZombieProgression : MonoBehaviour
{
    private Slider _slider;

    [SerializeField] private Image fillPicture;
    [SerializeField, Range(0, 1)] private float fadeDuration;

    private const float StartSpeed = 5f;
    public static float CurrentSpeed; 

    private TweenerCore<Color, Color, ColorOptions> _tweener;
    
    public delegate void EventHandler(object sender, EventArgs e);
    public event EventHandler GameOver;

    [SerializeField] private AnimationCurve progressSpeedCurve;
    private void Awake() {
        _slider = GetComponent<Slider>();
        CurrentSpeed = StartSpeed;
    }

    private void Update() {
        StartCoroutine(Progress());
    }

    private IEnumerator Progress() {
        _slider.value = CurrentSpeed - RoadsManager.CurrentSpeed;
        if(IsCloseOfMax() && _tweener is not { active: true }) ColorAnimation();
        else if(!IsCloseOfMax()) _tweener?.Kill();
        if (IsDead()) GameOver?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(0.1f);
    }

    private bool IsCloseOfMax() {
        return _slider.maxValue - _slider.value < _slider.maxValue / 10f;
    }

    private bool IsDead() {
        return _slider.maxValue - _slider.value < .1f;
    }

    private void ColorAnimation() {
        _tweener = fillPicture.DOColor(Color.red, fadeDuration);
        _tweener.onComplete += () => {
            fillPicture.DOColor(Color.white, fadeDuration).onComplete += () => ColorAnimation();
        };
    }

}