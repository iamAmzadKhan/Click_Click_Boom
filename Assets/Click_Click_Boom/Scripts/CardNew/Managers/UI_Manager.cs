using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;
    [SerializeField] 
    private TextMeshProUGUI scoreText;
    
   
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    

    void OnEnable() => Score_Manager.OnScoreChanged += UpdateScore;
    void OnDisable() => Score_Manager.OnScoreChanged -= UpdateScore;

    private void UpdateScore(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
    }
}
