using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Pool;
using UnityEngine.UI;
public class ZombieProgression : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private Image fillPicture;
    [SerializeField, Range(0, 1)] private float fadeDuration;
    private static float speedIncr = 1.5f;
    public static float zombieSpeed = 10;
    public static float basezombieSpeed = 10;
    private static float maxZombieSpeed = 130;
    private static float _distance = 1000;
    private static float _baseDistance = 1000;
    private TweenerCore<Color, Color, ColorOptions> _tweener;
    public delegate void EventHandler(object sender, EventArgs e);
    public event EventHandler GameOver;
    //[SerializeField] private AnimationCurve progressSpeedCurve;
    private void Awake() {
        _slider = GetComponent<Slider>();
        zombieSpeed = basezombieSpeed;
        _distance = _baseDistance;
        _slider.value = _slider.maxValue;
    }

    private void Start()
    {
        GameOver += (sender, args) => Character.Current.DisableInputs(); 
        StartCoroutine(Progress());
        GameOver += (sender, args) => StopCoroutine(Progress());
    }


    private IEnumerator Progress()
    {
        while (true)
        {
            zombieSpeed += speedIncr;
            if(IsCloseOfMax() && _tweener is not { active: true }) ColorAnimation();
            else if(!IsCloseOfMax()) _tweener?.Kill();
            var delta = RoadsManager.CurrentSpeed - zombieSpeed;
            _distance = Math.Clamp(_distance+=delta, 0, _baseDistance);
            UpdateSlider( _distance / _baseDistance);
            Debug.Log("Pouet = " + delta);
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateSlider(float distance)
    {
        if (distance > 0.08) {
            _slider.value = distance;
            Debug.Log("Ptitpouet");
        }

        if (IsCloseOfMax()) {
            Character.Current.Collided += () => GameOver?.Invoke(this, EventArgs.Empty);
        }
        else
            Character.Current.Collided -= () => GameOver?.Invoke(this, EventArgs.Empty);

    }
    private bool IsCloseOfMax() {
        return _slider.value <= 0.2f;
    }
    
    private void ColorAnimation() {
        _tweener = fillPicture.DOColor(Color.red, fadeDuration);
        _tweener.onComplete += () => {
            fillPicture.DOColor(Color.white, fadeDuration).onComplete += () => ColorAnimation();
        };
    }

}