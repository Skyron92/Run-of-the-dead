using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGame.Zombie
{
    [RequireComponent(typeof(Bird))]
    public class BirdMovement : MonoBehaviour
    {

        private Bird _birdReference;

        private float Left => _birdReference.GetBottomLeft().position.x;
        private float Right => _birdReference.GetTopRight().position.x;
        private float Up => _birdReference.GetTopRight().position.y;
        private float Down => _birdReference.GetBottomLeft().position.y;
        [SerializeField, Range(0.5f, 5f)] private float moveSpeed;

        private Vector2 _destination;

        private RectTransform _selfRectTransform;

        private TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore;
        private void Awake() {
            _selfRectTransform = GetComponent<RectTransform>();
            _birdReference = GetComponent<Bird>();
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