using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Declaration
    private static int _beerCount;
    private static int _cravateLevel;
    private static int _armeLevel;
    private static int _chaussureLevel;
    public delegate void Eventhandler();
    public static event Eventhandler BeerCountChanged;
    // Getter
    public static int GetBeerCount() => _beerCount;
    public static int GetCravateLevel() => _cravateLevel;
    public static int GetArmeLevel() => _armeLevel;
    public static int GetChaussureLevel() => _chaussureLevel;
    // Setter
    public static void SetBeerCount(int value)
    {
        _beerCount = value;
        BeerCountChanged?.Invoke();
    }
    public static void SetCravateLevel(int value) => _cravateLevel += value;
    public static void SetArmelevel(int value) => _armeLevel += value;
    public static void SetChaussureLevel(int value) => _chaussureLevel += value;

    // Method
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        SetBeerCount(0);
    }
    
    
    
}
