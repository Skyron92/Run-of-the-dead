using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlungerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private InputActionReference myInputPosition;
    [SerializeField] private InputActionReference myInputTouch;
    [SerializeField] private RectTransform plunger;
    [SerializeField] private RectTransform myPlayZone;
    private Vector2 InputPosition => myInputPosition.action.ReadValue<Vector2>();
    void Start()
    {
        if (!IsEverythingAssigned())
            Debug.LogError("Unassigned Serialized Variable in PlungerScript");
        EnablingInput();
    }
    
    private void EnablingInput()
    {
        myInputTouch.action.Enable();
        myInputPosition.action.Enable();
    }
    bool IsEverythingAssigned()
    {
        if (myInputPosition && myInputTouch && plunger && myPlayZone)
            return true;
        return false;
    }
    private bool IsPositioninPlayZone()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(myPlayZone, InputPosition);
    } 
    private bool IsPositionOnPlunger()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(plunger, InputPosition);
    }
    
    private void MovePlunger()
    {
        if (IsPositioninPlayZone() && IsPositionOnPlunger())
            plunger.position = new Vector2(plunger.position.x, InputPosition.y);
    }

    // Update is called once per frame
    private void Update()
    {
        if (myInputTouch.action.IsInProgress())
            MovePlunger();
    }
}
