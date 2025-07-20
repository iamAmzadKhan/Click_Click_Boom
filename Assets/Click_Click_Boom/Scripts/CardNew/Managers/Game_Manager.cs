using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance { get; private set; }

    public Score_Manager ScoreService { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ScoreService = GetComponent<Score_Manager>();
        if (ScoreService == null)
            Debug.LogError("ScoreManager is missing from GameManager GameObject.");
    }

    public void OnPairCleared()
    {
        
    }
}
