using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DetectCollide : MonoBehaviour, IMiniGame
{
    public event IMiniGame.MiniGameSuccessEvent MiniGameSuccess;
    [SerializeField] private Slider progressbar;
    private int _goal;
    private int _score;
    
    // Start is called before the first frame update
    void Start() {
        _goal = Random.Range(2, 9);
        progressbar.maxValue = _goal;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        // Victoire du mini-jeu
        if (_score >= _goal - 1) {
            MiniGameSuccess?.Invoke(this, MiniGameEventArgs.Empty);
        }
        else {
            _score++;
            progressbar.value++;
        }
    }
}
