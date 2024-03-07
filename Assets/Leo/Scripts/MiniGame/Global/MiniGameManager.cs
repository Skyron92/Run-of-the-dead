using System;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private IMiniGame _miniGameReference;
    private GameObject _miniGamePrefab;
    private GameObject _miniGameInstance;
    private GameObject _dialogBoxInstance;

    private void Start() {
        Character.Current.MgStarted += OnMGStarted;
    }
//coucou
    private void OnMGStarted(object sender, MgStartedEventArgs e) {
        SpawnDialogBox(e.DialogBoxPrefab);
        _miniGamePrefab = e.MgPrefab;
    }

    private void SpawnDialogBox(GameObject dialogBox) {
        _dialogBoxInstance = Instantiate(dialogBox);
        _dialogBoxInstance.GetComponentInChildren<Dialog>().DisplayEnded += OnDisplayEnded;
    }

    private void OnDisplayEnded(object sender, EventArgs e) {
        Debug.Log("Go ");
        Destroy(_dialogBoxInstance);
        SpawnMiniGame(_miniGamePrefab, out GameObject instance);
        _miniGameInstance = instance;
    }

    private void SpawnMiniGame(GameObject miniGame, out GameObject instance) {
        instance = Instantiate(miniGame);
        _miniGameReference = (IMiniGame)GameObject.FindGameObjectWithTag("MiniGame").GetComponent(typeof(IMiniGame));
        if (_miniGameReference == null)
            Debug.LogError("No mini-game reference found. Check if the script managing the mini-game is assigned " +
                           "to a game object with tag 'MiniGame', and this script must implements IMiniGame interface."); 
        BindEvent();
    }
    
    private void BindEvent() {
        if(_miniGameReference == null) return;
        _miniGameReference.MiniGameSuccess += OnSuccess;
    }

    private void OnSuccess(object sender, MiniGameEventArgs e) {
        
        StartBonus(e.Bonus);
        UnBindEvent();
        Destroy(_miniGameInstance);
    }
    
    private void UnBindEvent() {
        if(_miniGameReference == null) return;
        _miniGameReference.MiniGameSuccess -= OnSuccess;
    }

    private void StartBonus(Bonus bonus) {
        bonus.Do();
    }
}