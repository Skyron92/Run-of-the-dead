using System;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    public int BeerCount;
    public int CravateLevel;
    public int ArmeLevel;
    public int ChaussureLevel;
    public int Score;

    internal PlayerData(int beerCount, int cravateLevel, int armeLevel, int chaussureLevel, int score)
    {
        BeerCount = beerCount;
        CravateLevel = cravateLevel;
        ArmeLevel = armeLevel;
        ChaussureLevel = chaussureLevel;
        Score = score;
    }
}

public class GameManager : MonoBehaviour
{
    //Declaration
    private static int _beerCount;
    private static int _cravateLevel = 1;
    private static int _armeLevel = 1;
    private static int _chaussureLevel = 1;
    private static int _score = 0;
    private static bool _vibrationIsActive = true;
    public delegate void Eventhandler();
    public static event Eventhandler BeerCountChanged;
    public PlayerData _playerData;
    private string saveFilePath;
    
    // Getter
    public static int GetBeerCount() => _beerCount;
    public static int GetCravateLevel() => _cravateLevel;
    public static int GetArmeLevel() => _armeLevel;
    public static int GetChaussureLevel() => _chaussureLevel;

    public static int GetScore() => _score;
    
    public static bool GetVibration() => _vibrationIsActive;
    // Setter
    public static void SetBeerCount(int value)
    {
        _beerCount += value;
        BeerCountChanged?.Invoke();
    }
    public static void SetCravateLevel(int value) => _cravateLevel += value;
    public static void SetArmelevel(int value) => _armeLevel += value;
    public static void SetChaussureLevel(int value) => _chaussureLevel += value;
    public static void SetScore(int value) => _score = value;
    public static void SetVibration(bool value) => _vibrationIsActive = value;

    private GameManager _instance;
    
    // Method
    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this);
        else _instance = this;
        DontDestroyOnLoad(transform.gameObject);
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
        if (File.Exists(saveFilePath)) LoadPlayerData();
        BeerCountChanged?.Invoke();
        BeerCountChanged += SavePlayerData; // TKT
    }
    
    public void SavePlayerData() {
        _playerData = new PlayerData(_beerCount, _cravateLevel, _armeLevel, _chaussureLevel, _score);
        string savePlayerData = JsonUtility.ToJson(_playerData);
        File.WriteAllText(saveFilePath, savePlayerData);
        _playerData = null;
    }

    public void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            _playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
            _beerCount = _playerData.BeerCount;
            _armeLevel = _playerData.ArmeLevel;
            _chaussureLevel = _playerData.ChaussureLevel;
            _cravateLevel = _playerData.CravateLevel;
            _score = _playerData.Score;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus) SavePlayerData();
        else if (hasFocus) LoadPlayerData();
    }
}
