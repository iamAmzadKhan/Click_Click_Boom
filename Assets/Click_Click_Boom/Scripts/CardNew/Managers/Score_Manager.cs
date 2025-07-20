using System;
using UnityEngine;

public class Score_Manager : MonoBehaviour, IScoreService
{
    public int CurrentScore { get; private set; }
    public static event Action<int> OnScoreChanged;

    public void AddPoint(int pts = 1)
    {
        CurrentScore += pts;
        OnScoreChanged?.Invoke(CurrentScore);
    }
}
