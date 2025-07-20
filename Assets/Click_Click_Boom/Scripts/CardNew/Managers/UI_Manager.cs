using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void OnEnable() => Score_Manager.OnScoreChanged += UpdateScore;
    void OnDisable() => Score_Manager.OnScoreChanged -= UpdateScore;

    private void UpdateScore(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
    }
}
