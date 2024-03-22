using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public InputActionReference InputActionReference;
    public InputAction InputAction => InputActionReference.action;
    
    [SerializeField] private TextMeshProUGUI beerTMP;

    private void Start() {
        InputAction.Enable();
        InputAction.started += context => {
            GameManager.SetBeerCount(1);
            beerTMP.text = GameManager.GetBeerCount().ToString();
        };
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) GameManager.DeleteSaveFile();
    }
}
