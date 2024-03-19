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
    private static float speedIncr = 1.5f;
    public static double zombieSpeed = 10;
    private static float maxZombieSpeed = 130;
    private TweenerCore<Color, Color, ColorOptions> _tweener;
    public delegate void EventHandler(object sender, EventArgs e);
    public event EventHandler GameOver;
    //[SerializeField] private AnimationCurve progressSpeedCurve;
    private void Awake() {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        StartCoroutine(Progress());
    }


    private IEnumerator Progress()
    {
        while (true)
        {
            zombieSpeed += speedIncr;
            if(IsCloseOfMax() && _tweener is not { active: true }) ColorAnimation();
            else if(!IsCloseOfMax()) _tweener?.Kill();
            //Debug.Log("Zombie Speed = " + zombieSpeed);
            yield return new WaitForSeconds(1f);
            // if (IsDead()) {
            //     Character.Current.DisableInputs();
            //     GameOver?.Invoke(this, EventArgs.Empty);
            // }
        }
    }
    
    private bool IsCloseOfMax() {
        return _slider.maxValue - _slider.value < _slider.maxValue / 5f;
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