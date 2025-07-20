using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class MyUIManager : MonoBehaviour
{
    public static MyUIManager Instance { get; private set; }

    [Header("Screens")]
    [SerializeField] private List<UIScreen> screens;

    private Dictionary<string, UIScreen> screenMap = new();
    private UIScreen currentScreen;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

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
}
