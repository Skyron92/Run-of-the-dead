using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

public class SlideDetection : MonoBehaviour
{
    public Image hereImage;
    public Image notHerImage;

    private Sprite spriteHere;
    private Sprite spriteNotHere;
    
    public RectTransform globalMenu;
    
    [SerializeField] private InputActionReference myInput;
    private InputAction SlideInputAction => myInput.action; 
    Vector2 _delta => SlideInputAction.ReadValue<Vector2>();

    private float sensibility = .6f;

    public void DisableSlideInputAction()
    {
        SlideInputAction.Disable();
    }

    public void EnableSlideIpuntAction()
    {
        SlideInputAction.Enable();
    }
    void Start()
    {
        spriteHere = hereImage.sprite;
        spriteNotHere = notHerImage.sprite;
        
        SlideInputAction.Enable();
        SlideInputAction.started += context => Slide(); 
    }
    private void Slide()
    {
        if(globalMenu == null) return;
        if (_delta.x > 0 && globalMenu.anchorMax.x == 2)return;
        if (_delta.x < 0 && globalMenu.anchorMax.x == 1) return;
        if (_delta.y > 0.5f && _delta.y < -0.5f)return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (_delta.x >= sensibility) {
            if (hereImage.sprite != spriteNotHere) {
                ExchangeImages();
            }
            
            globalMenu.DOAnchorMax(new Vector2(2, globalMenu.anchorMax.y),0.25f);
            globalMenu.DOAnchorMin(new Vector2(1, globalMenu.anchorMin.y),0.25f);
        }
        if (_delta.x <= -sensibility) {
            if (hereImage.sprite != spriteHere) {
                ExchangeImages();
            }
            
            globalMenu.DOAnchorMax(new Vector2(1, globalMenu.anchorMax.y),0.25f);
            globalMenu.DOAnchorMin(new Vector2(0, globalMenu.anchorMin.y),0.25f);
        }
    }
    
    public void ExchangeImages() {
        Sprite temp = hereImage.sprite;
        hereImage.sprite = notHerImage.sprite;
        notHerImage.sprite = temp;
    }
}
