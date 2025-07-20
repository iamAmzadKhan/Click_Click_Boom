using System;
using System.IO;
using UnityEngine;

public class Score_Manager : MonoBehaviour, IScoreService, ITriesService
{
    public int CurrentScore { get; private set; }
    public int CurrentTotalScore { get; private set; }
    public int CurrentTries { get; private set; }  
    public int CurrentTotalTries { get; private set; }

    public int LevelScore { get; private set; }
    
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnTriesChanged;

    public PlayerData playerData;

    public string PlayerDataPath = "";
    
    public void AddPoint(int pts = 1)
    {
        CurrentScore += pts;
        CurrentTotalScore += pts;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void AddTries(int pts = 1)
    {
        CurrentTries += pts;
        CurrentTotalTries += pts;
        OnTriesChanged?.Invoke(CurrentTries);
    }
    public void Reset()
    {
        CurrentScore = 0;
        CurrentTries = 0;
        LevelScore = 0;
        OnScoreChanged?.Invoke(CurrentScore);
        OnTriesChanged?.Invoke(CurrentTries);
    }

    public void SetLevelScore(int levelScore)
    {
        LevelScore = levelScore;
    }

    void Start()
    {
        PlayerDataPath = Application.dataPath + "/PlayerSaveData.json";
    }

    public void SaveScore()
    {
        PlayerData playerData = new PlayerData(CurrentTotalTries,CurrentTotalTries);
        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(PlayerDataPath, savePlayerData);
    }

    public void LoadData()
    {
        PlayerData playerData;
        string loadPlayerData = File.ReadAllText(PlayerDataPath);
        playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
        CurrentTotalScore = playerData._Score;
        CurrentTotalTries = playerData._Tries;
    }
}


[System.Serializable]
public class PlayerData
{
    public int _Score;
    public int _Tries;
    
    public PlayerData(int score, int tries)
    {
        _Score = score;
        _Tries = tries;
    }
}
