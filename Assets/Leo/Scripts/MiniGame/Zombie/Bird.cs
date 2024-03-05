using DG.Tweening;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
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

        [Header("Die settings")]
        [SerializeField, Range(0.1f, 1f)] private float fallDuration;
        private RectTransform _selfRectTransform;
        [SerializeField] private RectTransform groundMinTransform;
        [SerializeField] private RectTransform zombieTransform;

        private bool _isDead;

        private int _life;
        private int Life {
            get { return _life; }
            set => _life = _life < 0 ? 0 : value;
        }

        private Image _image;
        [SerializeField, Range(0.1f, 1f)] private float fadeDuration;

        // Event when the player win
        public event IMiniGame.MiniGameWonEvent MiniGameWon;

        private void Awake() {
            CheckIfVariablesIsAssigned();
            _selfRectTransform = GetComponent<RectTransform>();
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
                if (!_isDead && PositionIsValid()) Hit();
            };
            TouchInputAction.canceled += context => {
                if(_isDead) Fall();
            };
        }

        private void Update() {
            if (!_isDead || !PositionIsValid() || !TouchInputAction.IsInProgress()) return;
            TrackInputPosition();
        }

        /// <summary>
        /// Follows the finger position
        /// </summary>
        private void TrackInputPosition() {
            if(_selfRectTransform == null) return;
            _selfRectTransform.position = InputPosition;
            if (!BirdIsOnZombie()) return;
            MiniGameWon?.Invoke(this, MiniGameEventArgs.Empty);
            Destroy(gameObject, 1f);
        }

        /// <summary>
        /// Check if the player touches the bird
        /// </summary>
        /// <returns>True if the player touches the bird.</returns>
        private bool PositionIsValid() {
            return _selfRectTransform != null && RectTransformUtility.RectangleContainsScreenPoint(_selfRectTransform, InputPosition);
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
            BirdMovement birdMovement = gameObject.GetComponent<BirdMovement>();
            birdMovement.StopMoving();
            Fall();
        }

        /// <summary>
        /// Fall of the bird
        /// </summary>
        private void Fall() {
            _selfRectTransform.DOMoveY(groundMinTransform.position.y, fallDuration);
        }

        private bool BirdIsOnZombie() {
            return RectTransformUtility.RectangleContainsScreenPoint(zombieTransform, _selfRectTransform.position);
        }
    }
}