using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniGame.Zombie {
    /// <summary>
    /// Manages the bird's behaviour
    /// </summary>
    public class Bird : MonoBehaviour{
        
        // Position input
        [SerializeField] private InputActionReference positionInputActionReference;
        private Vector2 InputPosition => positionInputActionReference.action.ReadValue<Vector2>();
        
        // Touch input
        [SerializeField] private InputActionReference touchInputActionReference;
        private InputAction TouchInputAction => touchInputActionReference.action;

        private RectTransform _selfRectTransform;
        [SerializeField] private RectTransform groundMinTransform;

        private bool _isDead;
        public bool IsDead {
            get => _isDead;
            set => _isDead = value;
        }

        private bool _canFall;

        private float TargetYPosition;
        
        [SerializeField, Range(0.1f, 1f)] private float fallDuration;

        private void Awake() {
            _selfRectTransform = GetComponent<RectTransform>();
            IsDead = false;
            positionInputActionReference.action.Enable();
            TouchInputAction.Enable();
            TouchInputAction.started += context => {
                if (!IsDead && PositionIsValid()) Die();
            };
            TouchInputAction.canceled += context => _canFall = _isDead;
        }

        private void Update() {
            if(IsDead && PositionIsValid() && TouchInputAction.IsInProgress()) TrackInputPosition();
            else {
                if (_canFall) {
                    Fall();   
                }
            }
        }

        /// <summary>
        /// Follows the finger position
        /// </summary>
        private void TrackInputPosition() {
            if (PositionIsValid()) {
                Debug.Log("Track");
                _canFall = false;
                _selfRectTransform.transform.position= InputPosition;
            }
        }

        /// <summary>
        /// Check if the player touches the bird
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool PositionIsValid() {
            return RectTransformUtility.RectangleContainsScreenPoint(_selfRectTransform, InputPosition);
        }

        /// <summary>
        /// Kill the bird
        /// </summary>
        private void Die() {
            _isDead = true;
            _canFall = true;
        }

        /// <summary>
        /// Fall of the bird
        /// </summary>
        private void Fall() {
            _selfRectTransform.transform.position = groundMinTransform.transform.position;
        }
    }
}