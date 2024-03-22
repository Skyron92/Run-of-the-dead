using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI beerTMP;
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) GameManager.DeleteSaveFile();
    }
}
