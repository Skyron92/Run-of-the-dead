using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

public class SlideDetection : MonoBehaviour
{
    public Image hereImage;
    public Image notHerImage;
    
    public RectTransform globalMenu;
    
    [SerializeField] private InputActionReference myInput;
    private InputAction SlideInputAction => myInput.action; 
    Vector2 _delta => SlideInputAction.ReadValue<Vector2>();
    
    // Start is called before the first frame update
    void Start()
    {
        SlideInputAction.Enable();
        SlideInputAction.started += context => Slide(); 
       // SlideInputAction.performed += context => ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Slide()
    {
        if (_delta.x>0 && globalMenu.anchorMax.x == 2)return;
        if (_delta.x<0 && globalMenu.anchorMax.x == 1)return;
        if (_delta.y>0.5 && _delta.y<-0.5)return;
        
        if (_delta.x > 0)
        {
                Invoke("ExchangeImages",0.5f);
            globalMenu.DOAnchorMax(new Vector2(2, globalMenu.anchorMax.y),0.25f);
            globalMenu.DOAnchorMin(new Vector2(1, globalMenu.anchorMin.y),0.25f);
        }
        if (_delta.x < 0)
        {
            Invoke("ExchangeImages",0.5f);
            globalMenu.DOAnchorMax(new Vector2(1, globalMenu.anchorMax.y),0.25f);
            globalMenu.DOAnchorMin(new Vector2(0, globalMenu.anchorMin.y),0.25f);
        }
    }
    
    public void ExchangeImages()
    {
        Sprite temp = hereImage.sprite;
        hereImage.sprite = notHerImage.sprite;
        notHerImage.sprite = temp;
    }
}
