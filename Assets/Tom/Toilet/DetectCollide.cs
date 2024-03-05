using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DetectCollide : MonoBehaviour, IMiniGame
{
    public event IMiniGame.MiniGameWonEvent MiniGameWon;
    [SerializeField] private Slider progressbar;
    private int _goal = 0;
    private int _score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _goal = Random.Range(2, 9);
        progressbar.maxValue = _goal;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Victoire du mini-jeu
        if (_score >= _goal)
        {
            MiniGameWon?.Invoke(this, MiniGameEventArgs.Empty);
        }
        else
        {
            _score++;
            progressbar.value++;
        }

        Debug.Log("collided");
    }
}
