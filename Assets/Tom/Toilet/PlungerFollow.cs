using UnityEngine;
using UnityEngine.InputSystem;

public class PlungerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private InputActionReference myInputPosition;
    [SerializeField] private InputActionReference myInputTouch;
    [SerializeField] private RectTransform plunger;
    [SerializeField] private RectTransform myPlayZone;
    [SerializeField] private RectTransform touchzone;
    private bool isTracking = false;
    
    private Vector2 InputPosition => myInputPosition.action.ReadValue<Vector2>();
    
    void Start()
    {
        // Verification si les variable serializefield son assignée dans l'éditeur
        if (!IsEverythingAssigned())
            Debug.LogError("Unassigned Serialized Variable in PlungerScript");
        EnablingInput();
        myInputTouch.action.started += context =>
        {
            if (IsPositionOnPlunger())
                isTracking = true;
        };
        myInputTouch.action.canceled += context => isTracking = false;
    }
    
    private void EnablingInput()
    {
        myInputTouch.action.Enable();
        myInputPosition.action.Enable();
    }
    private bool IsEverythingAssigned()
    {
        if (myInputPosition && myInputTouch && plunger && myPlayZone && touchzone)
            return true;
        return false;
    }
    /// <summary>
    /// Permet de verifier si le déplacement de l'objet est valide dans l'espace de jeu.
    /// </summary>
    /// <returns></returns>
    private bool IsPositioninPlayZone()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(myPlayZone, InputPosition);
    }
    
    /// <summary>
    /// Permet de verifier si le touche l'objet et pas n'importe ou sur l'écran.
    /// </summary>
    /// <returns></returns>
    private bool IsPositionOnPlunger()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(touchzone, InputPosition);
    }
    
    private void MovePlunger()
    {
        if (IsPositioninPlayZone())
            plunger.position = new Vector2(plunger.position.x, InputPosition.y);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (myInputTouch.action.IsInProgress() && isTracking )
            MovePlunger();
    }
}
