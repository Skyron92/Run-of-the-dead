using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MiniGame.Zombie {
    /// <summary>
    /// Manages the bird's behaviour
    /// </summary>
    public class Bird : MonoBehaviour, IMiniGame {
        
        [Header("Inputs settings")]
        // Position input
        [SerializeField] private InputActionReference positionInputActionReference;
        private Vector2 InputPosition => positionInputActionReference.action.ReadValue<Vector2>();

        // Touch input
        [SerializeField] private InputActionReference touchInputActionReference;
        private InputAction TouchInputAction => touchInputActionReference.action;

        [SerializeField] private RectTransform detectionAreaTransform;

        [Header("Die settings")]
        [SerializeField, Range(0.1f, 1f)] private float fallDuration;
        private RectTransform _selfRectTransform;
        [SerializeField] private RectTransform groundMinTransform;
        [SerializeField] private RectTransform zombieTransform;

        private bool _isDead;
        private bool _isTracking;

        private int _life;
        private int Life {
            get { return _life; }
            set => _life = _life < 0 ? 0 : value;
        }

        private Image _image;
        [SerializeField, Range(0.1f, 1f)] private float fadeDuration;
        
        [Header("Move settings")]
        [SerializeField] private RectTransform bottomLeftTransform;
        [SerializeField] private RectTransform topRightTransform;
        private float Left => bottomLeftTransform.position.x;
        private float Right => topRightTransform.position.x;
        private float Up => topRightTransform.position.y;
        private float Down => bottomLeftTransform.position.y;

        private readonly Bike _bike = new Bike();
        
        private Vector3 BirdPosition {
            get => _selfRectTransform.position;
            set => _selfRectTransform.position = GetClampedPosition(value);
        }

        private bool _miniGameEnded;

        private TweenerCore<Vector3, Vector3, VectorOptions> _tween;

        // Event when the player win
        public event IMiniGame.MiniGameSuccessEvent MiniGameSuccess;

        private void Awake() {
            CheckIfVariablesIsAssigned();
            _selfRectTransform = GetComponent<RectTransform>();
            Debug.Log(detectionAreaTransform);
            SetupInputs();
            Life = Random.Range(1, 3);
            _image = GetComponent<Image>();
        }

        private void CheckIfVariablesIsAssigned() {
            if (!groundMinTransform || !positionInputActionReference || !touchInputActionReference || !zombieTransform)
                Debug.LogError("You have unassigned variables. Check the Bird script on the " + name + " gameObject.");
        }

        private void SetupInputs() {
            positionInputActionReference.action.Enable();
            TouchInputAction.Enable();
            TouchInputAction.started += context => {
                switch (_isDead) {
                    case false when PositionIsValid():
                        Hit();
                        break;
                    case true when PositionIsValid():
                        StopFall();
                        _isTracking = true;
                        break;
                }
            };
            TouchInputAction.canceled += context => {
                if(_isDead) Fall();
                _isTracking = false;
            };
        }

        private void FixedUpdate() {
            if (!_isDead || !_isTracking || !TouchInputAction.IsInProgress()) return;
            TrackInputPosition();
        }

        /// <summary>
        /// Follows the finger position
        /// </summary>
        private void TrackInputPosition() {
            if(_selfRectTransform == null) return;
            if (IsOnZombie(BirdPosition)) {
                MiniGameSuccess?.Invoke(this, new MiniGameEventArgs(_bike));
                _miniGameEnded = true;
                Destroy(gameObject, 1f);
            }
            else BirdPosition = GetClampedPosition(InputPosition);
        }

        private Vector2 GetClampedPosition(Vector2 value) {
            return new Vector2(Mathf.Clamp(value.x, Left, Right), Mathf.Clamp(value.y, Down, Up));
        }

        /// <summary>
        /// Check if the player touches the bird
        /// </summary>
        /// <returns>True if the player touches the bird.</returns>
        private bool PositionIsValid() {
            return detectionAreaTransform != null && RectTransformUtility.RectangleContainsScreenPoint(detectionAreaTransform, InputPosition);
        }

        void Hit() {
            _image.DOColor(Color.red, fadeDuration).onComplete += () => _image.DOColor(Color.white, fadeDuration);
            Life--;
            if(Life == 0) Die();
        }

        /// <summary>
        /// Kill the bird
        /// </summary>
        private void Die() {
            _isDead = true;
            GetComponent<BirdMovement>().StopMoving();
            Fall();
        }

        /// <summary>
        /// Makes the bird fall
        /// </summary>
        private void Fall() {
            if(_miniGameEnded) return;
            _tween = _selfRectTransform.DOMoveY(groundMinTransform.position.y, fallDuration);
        }

        /// <summary>
        /// Interrupts the fall
        /// </summary>
        private void StopFall() {
            if(_tween == null) return;
            _tween.Kill();
        }

        /// <summary>
        /// Check if an element is on the zombie's panel
        /// </summary>
        /// <param name="position">Position of the element to check</param>
        /// <returns></returns>
        private bool IsOnZombie(Vector2 position) {
            return RectTransformUtility.RectangleContainsScreenPoint(zombieTransform, position);
        }

       
    }
}