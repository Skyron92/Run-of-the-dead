using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    [SerializeField] private InputActionReference slideInputActionReference;
    private InputAction slideInputAction => slideInputActionReference.action;

    [SerializeField] private List<Transform> spots = new List<Transform> ();

    [SerializeField, Range(0, 1)] private float offsetSpeed;
    private int _actualSpot;
    public int actualSpot {
        get { return _actualSpot; }
        set { 
            if (value < 0) _actualSpot = 0;
            if (value > 2) _actualSpot = 2;
        }
    }

    private bool _isMoving;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isMoving) Move();
    }

    public void SetDestination(int modifier)
    {
        if (_actualSpot == 0 && modifier == -1 || _actualSpot == 2 && modifier == 1) return;
        _actualSpot += modifier;
        _isMoving = true;
    }

    public void Move()
    {
        transform.position = Vector3.Lerp(transform.position, spots[_actualSpot].position, offsetSpeed);
        if (Vector3.Distance(transform.position, spots[_actualSpot].position) <= .1f) _isMoving = false;
    }
}
