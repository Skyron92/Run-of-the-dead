using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private IMiniGame _miniGameReference;
    private GameObject _miniGamePrefab;
    private GameObject _miniGameInstance;

    private void Start() {
        Character.Current.MgStarted += OnMGStarted;
    }
//coucou
    private void OnMGStarted(object sender, MgStartedEventArgs e) {
        SpawnMiniGame(e.MgPrefab, out GameObject instance);
        _miniGameInstance = instance;
    }

    private void SpawnMiniGame(GameObject miniGame, out GameObject instance) {
        instance = Instantiate(miniGame);
        _miniGameReference = (IMiniGame)GameObject.FindGameObjectWithTag("MiniGame").GetComponent(typeof(IMiniGame));
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