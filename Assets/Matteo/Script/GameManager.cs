using System.IO;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerData 
{
    public readonly int BeerCount;
    public readonly int CravateLevel;
    public readonly int ArmeLevel;
    public readonly int ChaussureLevel;

    internal PlayerData(int beerCount, int cravateLevel, int armeLevel, int chaussureLevel)
    {
        BeerCount = beerCount;
        CravateLevel = cravateLevel;
        ArmeLevel = armeLevel;
        ChaussureLevel = chaussureLevel;
    }
}


public class GameManager : MonoBehaviour
{
    //Declaration
    private static int _beerCount;
    private static int _cravateLevel;
    private static int _armeLevel;
    private static int _chaussureLevel;
    public delegate void Eventhandler();
    public static event Eventhandler BeerCountChanged;
    private PlayerData _playerData;
    private string saveFilePath;
    
    // Getter
    public static int GetBeerCount() => _beerCount;
    public static int GetCravateLevel() => _cravateLevel;
    public static int GetArmeLevel() => _armeLevel;
    public static int GetChaussureLevel() => _chaussureLevel;
    // Setter
    public static void SetBeerCount(int value)
    {
        _beerCount += value;
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
        Debug.Log("ABeer + " + GetBeerCount());
        Debug.Log("ACravate + " + GetCravateLevel());
        Debug.Log("AArme + " + GetArmeLevel());
        Debug.Log("AChaussure + " + GetChaussureLevel());
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            SavePlayerData();
        if (Input.GetKeyDown(KeyCode.L))
            LoadPlayerData();
    }
    private void SavePlayerData()
    {
        Debug.Log("SaveFilePath = " + saveFilePath);
        SetBeerCount(500);
        SetChaussureLevel(1);
        SetCravateLevel(1);
        SetArmelevel(1);
        _playerData = new PlayerData(_beerCount, _cravateLevel, _armeLevel, _chaussureLevel);
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
        string savePlayerData = JsonUtility.ToJson(_playerData);
        File.WriteAllText(saveFilePath, savePlayerData);
        _playerData = null;
    }

    private void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            _playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
        }

        Debug.Log("Beer + " + GetBeerCount());
        Debug.Log("Cravate + " + GetCravateLevel());
        Debug.Log("Arme + " + GetArmeLevel());
        Debug.Log("Chaussure + " + GetChaussureLevel());

    }
}
