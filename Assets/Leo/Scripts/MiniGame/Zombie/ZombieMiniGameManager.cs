using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniGame.Zombie
{
    public class ZombieMiniGameManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference touchInputActionReference;
        private InputAction TouchInputAction => touchInputActionReference.action;

        [SerializeField] private InputActionReference positionInputActionReference;
        private Vector2 InputPosition => positionInputActionReference.action.ReadValue<Vector2>();

        [SerializeField] private RectTransform birdTransform;

        [SerializeField] private Bird birdReference;

        private void Awake() {
            TouchInputAction.Enable();
            positionInputActionReference.action.Enable();
        
        }

        private void Update()
        {
            Debug.Log(InputPosition);
        }

        public void CheckBirdPosition() {
        }
    }
}