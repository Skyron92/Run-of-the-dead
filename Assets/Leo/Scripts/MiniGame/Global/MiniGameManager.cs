using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the mini games
/// </summary>
public class MiniGameManager : MonoBehaviour
{
    private IMiniGame _miniGameReference;
    private GameObject _miniGamePrefab;
    private GameObject _miniGameInstance;
    private GameObject _dialogBoxInstance;

    [SerializeField] private Button pauseButton;

    public delegate void EventHandler();

    public static event EventHandler Victory; 

    private void Start() {
        // Subscribe to the MGStarted event
        Character.Current.MgStarted += OnMGStarted;
        Character.Current.Dead += OnDead;
    }

    private void OnDead() {
        if(_miniGameInstance) Destroy(_miniGameInstance);
        if(_dialogBoxInstance) Destroy(_dialogBoxInstance);
    }

    /// <summary>
    /// Called when a PNJ is triggered
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMGStarted(object sender, MgStartedEventArgs e) {
        if(RunnerManager.IsEnded()) return;
        Character.Current.enabled = false;
        RoadsManager.StopMovement();
        SpawnDialogBox(e.DialogBoxPrefab, e.Headsprite);
        _miniGamePrefab = e.MgPrefab;
        pauseButton.interactable = false;
    }

    /// <summary>
    /// Display the PNJ dialog
    /// </summary>
    /// <param name="dialogBox"></param>
    private void SpawnDialogBox(GameObject dialogBox, Sprite headSprite) {
        Debug.Log(dialogBox);
        _dialogBoxInstance = Instantiate(dialogBox);
        var dialog = _dialogBoxInstance.GetComponentInChildren<Dialog>();
        dialog.SetSprite(headSprite);
        dialog.DisplayEnded += OnDisplayEnded;
    }

    /// <summary>
    /// Called when the dialog is ended
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDisplayEnded(object sender, EventArgs e) {
        Destroy(_dialogBoxInstance);
        SpawnMiniGame(_miniGamePrefab, out GameObject instance);
        _miniGameInstance = instance;
    }

    /// <summary>
    /// Start the mini game
    /// </summary>
    /// <param name="miniGame">Mini game prefab</param>
    /// <param name="instance">Instance of the mini game to harvest</param>
    private void SpawnMiniGame(GameObject miniGame, out GameObject instance) {
        if (RunnerManager.IsEnded()) {
            instance = null;
            return;
        }
        instance = Instantiate(miniGame);
        _miniGameReference = (IMiniGame)GameObject.FindGameObjectWithTag("MiniGame").GetComponent(typeof(IMiniGame));
        if (_miniGameReference == null) Debug.LogError("No mini-game reference found. Check if the script managing the mini-game is assigned " +
                                                       "to a game object with tag 'MiniGame', and this script must implements IMiniGame interface."); 
        BindEvent();
    }
    /// <summary>
    /// Subscribe to the success event
    /// </summary>
    private void BindEvent() {
        if(_miniGameReference == null) return;
        _miniGameReference.MiniGameSuccess += OnSuccess;
    }

    /// <summary>
    /// Called when the mini game is clear
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSuccess(object sender, MiniGameEventArgs e) {
        Invoke("EnablePlayerInput", 0.1f);
        Destroy(_miniGameInstance);
        RoadsManager.StartMovement();
        StartBonus(e.Bonus);
        UnBindEvent();
        pauseButton.interactable = true;
        Victory?.Invoke();
    }

    private void EnablePlayerInput() => Character.Current.EnableInputs();
    
    /// <summary>
    /// Unsubscribe to the success event
    /// </summary>
    private void UnBindEvent() {
        if(_miniGameReference == null) return;
        _miniGameReference.MiniGameSuccess -= OnSuccess;
    }

    /// <summary>
    /// Start the bonus for the player
    /// </summary>
    /// <param name="bonus">Bonus to activate</param>
    private void StartBonus(Bonus bonus) {
        bonus.Do();
    }
}