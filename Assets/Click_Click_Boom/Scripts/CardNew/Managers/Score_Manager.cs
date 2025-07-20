using System;
using UnityEngine;

public class Score_Manager : MonoBehaviour, IScoreService, ITriesService
{
    public int CurrentScore { get; private set; }
    public int CurrentTries { get; private set; }

    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnTriesChanged;

    public void AddPoint(int pts = 1)
    {
        CurrentScore += pts;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void AddTries(int pts = 1)
    {
        CurrentScore += pts;
        OnTriesChanged?.Invoke(CurrentTries);
    }
}
