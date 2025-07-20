using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [Header("Screens")]
    [SerializeField] private List<UIScreen> screens;

    private Dictionary<string, UIScreen> screenMap = new();
    private UIScreen currentScreen;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    void OnEnable() => Score_Manager.OnScoreChanged += UpdateScore;
    void OnDisable() => Score_Manager.OnScoreChanged -= UpdateScore;

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

        foreach (var screen in screens)
        {
            screenMap[screen.ScreenName] = screen;
            screen.gameObject.SetActive(false);
        }
        ShowScreen("Splash");
    }

    public void ShowScreen(string name)
    {
        if (currentScreen != null)
            currentScreen.gameObject.SetActive(false);

        if (screenMap.TryGetValue(name, out var screen))
        {
            screen.gameObject.SetActive(true);
            currentScreen = screen;
        }
        else
        {
            Debug.LogWarning($"Screen '{name}' not found.");
        }
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
    }
}
