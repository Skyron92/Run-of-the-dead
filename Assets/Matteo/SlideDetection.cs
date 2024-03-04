using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.TextCore.LowLevel;

public class SlideDetection : MonoBehaviour
{

    public RectTransform globalMenu;
    
    [SerializeField] private InputActionReference myInput;
    private InputAction SlideInputAction => myInput.action; 
    Vector2 _delta => SlideInputAction.ReadValue<Vector2>();
    
    // Start is called before the first frame update
    void Start()
    {
        SlideInputAction.Enable();
        SlideInputAction.started += context => GaySlide(); 
       // SlideInputAction.performed += context => ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GaySlide()
    {
        if (_delta.x>0 && globalMenu.anchorMax.x == 2)return;
        if (_delta.x<0 && globalMenu.anchorMax.x == 1)return;
        if (_delta.y>0.5 && _delta.y<-0.5)return;
        if (_delta.x > 0)
        {
            globalMenu.DOAnchorMax(new Vector2(2, globalMenu.anchorMax.y),0.25f);
            globalMenu.DOAnchorMin(new Vector2(1, globalMenu.anchorMin.y),0.25f);
        }
        if (_delta.x < 0)
        {
            globalMenu.DOAnchorMax(new Vector2(1, globalMenu.anchorMax.y),0.25f);
            globalMenu.DOAnchorMin(new Vector2(0, globalMenu.anchorMin.y),0.25f);
        }
    }
}
