using System;
using UnityEngine;

/// <summary>
/// Manages the mini games
/// </summary>
public class MiniGameManager : MonoBehaviour
{
    private IMiniGame _miniGameReference;
    private GameObject _miniGamePrefab;
    private GameObject _miniGameInstance;
    private GameObject _dialogBoxInstance;

    // Gameplay buttons of the runner
    [SerializeField] private GameObject runnerButtons;

    private void Start() {
        // Subscribe to the MGStarted event
        Character.Current.MgStarted += OnMGStarted;
    }
    /// <summary>
    /// Called when a PNJ is triggered
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMGStarted(object sender, MgStartedEventArgs e) {
        runnerButtons.SetActive(false);
        Character.Current.enabled = false;
        RoadsManager.StopMovement();
        SpawnDialogBox(e.DialogBoxPrefab);
        _miniGamePrefab = e.MgPrefab;
    }

    /// <summary>
    /// Display the PNJ dialog
    /// </summary>
    /// <param name="dialogBox"></param>
    private void SpawnDialogBox(GameObject dialogBox) {
        _dialogBoxInstance = Instantiate(dialogBox);
        _dialogBoxInstance.GetComponentInChildren<Dialog>().DisplayEnded += OnDisplayEnded;
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
        runnerButtons.SetActive(true);
        Character.Current.enabled = true;
        RoadsManager.StartMovement();
        StartBonus(e.Bonus);
        UnBindEvent();
        Destroy(_miniGameInstance);
    }
    
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