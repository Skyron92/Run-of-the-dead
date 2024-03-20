using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DetectCollide : MonoBehaviour, IMiniGame
{
    public event IMiniGame.MiniGameSuccessEvent MiniGameSuccess;
    [SerializeField] private Slider progressbar;
    [SerializeField] private ParticleSystem waterDropEffect;
    private int _goal;
    private int _score;

    private Bike bonus = new Bike();
    
    // Start is called before the first frame update
    void Start() {
        _goal = Random.Range(2, 9);
        progressbar.maxValue = _goal;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(waterDropEffect.isPlaying) waterDropEffect.Stop();
        waterDropEffect.Play();
        // Victoire du mini-jeu
        if (_score >= _goal - 1) {
            MiniGameSuccess?.Invoke(this, new MiniGameEventArgs(bonus));
        }
        else {
            _score++;
            progressbar.value++;
        }
    }
}
