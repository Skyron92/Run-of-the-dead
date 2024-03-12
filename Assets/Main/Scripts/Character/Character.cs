using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine.InputSystem;

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
    private Vector2 SlideInputValue => SlideInputAction.ReadValue<Vector2>();
    
    // Current position index
    private int _actualSpot = 1;
    // Properties for the actual spot, allows us to keep the value between 0 and 2
    private int ActualSpot {
        get {
            return _actualSpot;
        }
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

    // True if the character is switching of position
    private bool _isMoving;
    
    private bool _isGrounded = true;

    private void Awake() {
        // Set the initial value at 1, the middle spot point index
        ActualSpot = 1;
        Current = this;
        SlideInputAction.Enable();
        SlideInputAction.started += context => {
            OnSlide();
        };
       /* SlideInputAction.started += context => {
            Debug.Log("Slide");
           if(!_isGrounded) Jump();
        };*/
    }

    private void OnSlide() {
        if(!_canMove) return;
        // To the left
        if (Mathf.Abs(SlideInputValue.y) >= Mathf.Abs(SlideInputValue.x) && _isGrounded && SlideInputValue.y >= .4f) {
            Jump();
            return;
        }
        if(_canMove) { 
            if (SlideInputValue.x <= -.6f) {
                SetDestination(-1);
            }
            if(SlideInputValue.x >= .6f) SetDestination(1);
        }
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
        transform.DOMoveX(spots[_actualSpot].position.x, offsetSpeed, true).onComplete += () => _canMove = true;
    }

    private void Jump() {
        _canMove = false;
       transform.DOMoveY(6.5f, .2f, true).onComplete += () => {
           _canMove = true;
           transform.DOMoveY(1.8f, .2f, true).onComplete += () => _isGrounded = true;
       };
       _isGrounded = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PNJ")) {
            PNJ pnj = other.gameObject.GetComponent<PNJ>();
            MgStarted?.Invoke(this, new MgStartedEventArgs(pnj.GetMGPrefab(), pnj.GetDiologBox()));
        }
    }
}