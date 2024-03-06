using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private IMiniGame _miniGameReference;
    private GameObject _miniGamePrefab;
    private GameObject _miniGameInstance;

    private void Start() {
        Character.Current.MgStarted += OnMGStarted;
    }

    private void OnMGStarted(object sender, MgStartedEventArgs e) {
        SpawnMiniGame(e.MgPrefab, out GameObject instance);
        _miniGameInstance = instance;
    }

    private void SpawnMiniGame(GameObject miniGame, out GameObject instance) {
        instance = Instantiate(miniGame);
        _miniGameReference = instance.GetComponent<IMiniGame>();
        BindEvent();
    }
    
    private void BindEvent() {
        if(_miniGameReference == null) return;
        _miniGameReference.MiniGameSuccess += OnSuccess();
    }

    private IMiniGame.MiniGameSuccessEvent OnSuccess() {
        UnBindEvent();
        Destroy(_miniGameInstance);
        StartBonus();
        return null;
    }
    
    private void UnBindEvent() {
        if(_miniGameReference == null) return;
        _miniGameReference.MiniGameSuccess -= OnSuccess();
    }
    
    private void StartBonus(){}
}