using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the player character
/// </summary>
public class Character : MonoBehaviour
{
    // List of the spots available for the player movement
    [SerializeField] private List<Transform> spots = new List<Transform> ();
    // Speed of the right-left slide character
    [SerializeField, Range(0, 1)] private float offsetSpeed;

    [SerializeField] private InputActionReference slideInputActionReference;
    [SerializeField] private InputActionReference tapInputActionReference;
    private InputAction SlideInputAction => slideInputActionReference.action;
    private InputAction TapInputAction => tapInputActionReference.action;
    private Vector2 SlideInputValue => SlideInputAction.ReadValue<Vector2>();
    private Vector2 _swipeDirection;

    private float _minimumSwipeMagnitude;
    private float _sensibility;
    
    // Current position index
    private int _actualSpot = 1;
    // Properties for the actual spot, allows us to keep the value between 0 and 2
    private int ActualSpot {
        get => _actualSpot;
        set {
            _actualSpot = value switch {
                < 0 => 0,
                > 2 => 2,
                _   => value
            };
        }
    }

    public static Character Current;

    private bool _canMove = true;
    public delegate void MgStartedEvent(object sender, MgStartedEventArgs e);

    public event MgStartedEvent MgStarted;

    public delegate void EventHandler();

    public event EventHandler BeerCollected;
    public event EventHandler Dead;
    public event EventHandler Collided;
    
    // True if the character is switching of position
    private bool IsMoving => !(Vector3.Distance(transform.position, spots[_actualSpot].position) < 0.01f);

    private Animator _animatorController;

    [HideInInspector] public bool isInvincible;
    [HideInInspector] public bool isBoosted;
    [HideInInspector] public bool hasReleased = true;

    private TweenerCore<Vector3, Vector3, VectorOptions> jumpTweener;
    private TweenerCore<Vector3, Vector3, VectorOptions> moveTweener;
    
    private void Awake() {
        // Set the initial value at 1, the middle spot point index
        ActualSpot = 1;
        Current = this;
        SceneManager.sceneLoaded += (arg0, mode) => EnableInputs();
        SlideInputAction.started += context => {
            if(EventSystem.current.IsPointerOverGameObject()) return;
            DeterminesDirection();
        };
        TapInputAction.canceled += context => hasReleased = true;
        _animatorController = GetComponentInChildren<Animator>();
    }

    private void DeterminesDirection() {
        if(Mathf.Abs(SlideInputValue.magnitude) < _minimumSwipeMagnitude || !hasReleased) return;
        hasReleased = false;
        if (Math.Abs(SlideInputValue.x) > _swipeDirection.y) {
            if(SlideInputValue.x > _sensibility) SetDestination(1);
            if (SlideInputValue.x < _sensibility) SetDestination(-1);
            return;
        }
        if (SlideInputValue.y > _sensibility) Jump();
    }

    /// <summary>
    /// The character movement start by calling this method.
    /// Set this method on a button
    /// </summary>
    /// <param name="modifier">Set this to -1 if you wanna go to the left, 1 if you wanna go to the right</param>
    public void SetDestination(int modifier) {
        // If the player is on the left side & go to the left or on the right side & go to the right, do nothing
        if (_actualSpot == 0 && modifier == -1 || _actualSpot == 2 && modifier == 1) return;
        // Modify the current index position
        ActualSpot += modifier;
        Move();
    }

    /// <summary>
    /// Executes the character movement
    /// </summary>
    private void Move() {
        if(!_canMove) return;
        _canMove = false;
        moveTweener = transform.DOMoveX(spots[_actualSpot].position.x, offsetSpeed, true);
        moveTweener.onComplete += () => {
            _canMove = true;
            moveTweener.Kill();
        };
    }

    public void Jump() {
        _canMove = false;
       jumpTweener = transform.DOMoveY(6f, .3f, true);
        jumpTweener.onComplete += () => {
           _canMove = true;
           transform.DOMoveY(1.8f, .3f, true).onComplete += () => jumpTweener.Kill();
       };
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PNJ")) {
            if(isBoosted) return;
            PNJ pnj = other.gameObject.GetComponent<PNJ>();
            DisableInputs();
            MgStarted?.Invoke(this, new MgStartedEventArgs(pnj.GetMGPrefab(), pnj.GetDiologBox()));
        }

        if (other.CompareTag("Beer")) {
            Destroy(other.gameObject);
           // Debug.Log("Beer : " + RunnerManager.GetBeerCollected());
            BeerCollected?.Invoke();
        }

        if (other.CompareTag("Obstacle")) {
            if(isInvincible) return;
            Collided?.Invoke();
            RoadsManager.SlowDown();
            _animatorController.SetTrigger(_animatorController.GetParameter(0).name);
            Camera.main.DOShakePosition(1f, Vector3.one * 0.1f);
            if(GameManager.GetVibration()) Handheld.Vibrate();
        }

        if (other.CompareTag("Fatal")) {
            if(isInvincible) return;
            isInvincible = true;
            Camera.main.DOShakePosition(1f, Vector3.one * 0.8f);
            RoadsManager.StopMovement(0);
            if(GameManager.GetVibration()) Handheld.Vibrate();
            DisableInputs();
            Dead?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Obstacle")) _animatorController.ResetTrigger("Hurt");
    }

    public void DisableInputs() {
        TapInputAction.Disable();
        SlideInputAction.Disable();
    }
    
    public void EnableInputs() {
        TapInputAction.Enable();
        SlideInputAction.Enable();
    }

    private void OnDestroy() {
        moveTweener?.Kill();
        jumpTweener?.Kill();
    }
}