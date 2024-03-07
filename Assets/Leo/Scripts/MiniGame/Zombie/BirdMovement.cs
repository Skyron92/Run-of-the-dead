using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGame.Zombie
{
    /// <summary>
    /// Manages the free movement of the bird (before it dies)
    /// </summary>
    [RequireComponent(typeof(Bird))]
    public class BirdMovement : MonoBehaviour
    {
        [Header("Move settings")]
        [SerializeField] private RectTransform bottomLeftTransform;
        [SerializeField] private RectTransform topRightTransform;
        private float Left => bottomLeftTransform.position.x;
        private float Right => topRightTransform.position.x;
        private float Up => topRightTransform.position.y;
        private float Down => bottomLeftTransform.position.y;
        
        [SerializeField, Range(0.5f, 5f)] private float moveSpeed;

        private Vector2 _destination;

        private RectTransform _selfRectTransform;

        private TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore;
        private void Awake() {
            _selfRectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable() {
            Move();
        }

        private void Move() {
            SetUpDestination();
            tweenerCore = _selfRectTransform.DOMove(_destination, moveSpeed);
            tweenerCore.onComplete += () => Move();
        }

        public void StopMoving() {
            tweenerCore.Kill();
            Destroy(this);
        }
        
        private void SetUpDestination() {
            _destination = new Vector2(Random.Range(Left, Right), Random.Range(Down, Up));
        }
    }
}