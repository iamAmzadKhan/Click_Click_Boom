using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScreen : MonoBehaviour
{
    public TMP_Text m_ScoreText;
    public TMP_Text m_TriesText;

    public TMP_Text m_TotalScoreText;
    public TMP_Text m_TotalTriesText;

    public Button m_PlayAgainButton;
    public Button m_MainMenuButton;
    public Button m_QuitButton;
    void Start()
    {
        m_PlayAgainButton.onClick.AddListener(OnPlayAgain);
        m_MainMenuButton.onClick.AddListener(OnMainMenu);
        m_QuitButton.onClick.AddListener(OnQuit);
        ShowStats();
    }

    private void OnEnable()
    {
        ShowStats();
    }
    void ShowStats()
    {
        if (m_ScoreText != null)
        {
            if (Game_Manager.Instance != null)
            {
                m_ScoreText.text = Game_Manager.Instance.ScoreService.CurrentScore.ToString();
            }
        }

        if (m_TriesText != null)
        {
            if (Game_Manager.Instance != null)
            {
                m_TriesText.text = Game_Manager.Instance.ScoreService.CurrentTries.ToString();
            }
        }

        if (m_TotalScoreText != null)
        {
            if (Game_Manager.Instance != null)
            {
                m_TotalScoreText.text = Game_Manager.Instance.ScoreService.CurrentTotalScore.ToString();
            }
        }

        if (m_TotalTriesText != null)
        {
            if (Game_Manager.Instance != null)
            {
                m_TotalTriesText.text = Game_Manager.Instance.ScoreService.CurrentTotalTries.ToString();
            }
        }
    }
    void OnPlayAgain()
    {
        if (UI_Manager.Instance != null)
        {
            UI_Manager.Instance.ShowScreen("LevelSelection");
        }
        ClearStats();
    }

    void OnMainMenu()
    {
        if (UI_Manager.Instance != null)
        {
            UI_Manager.Instance.ShowScreen("MainMenu");
        }
        ClearStats() ;
    }
    void OnQuit()
    {
        Application.Quit();
    }
    void ClearStats()
    {
        if(Game_Manager.Instance != null)
        {
            Game_Manager.Instance.ScoreService.Reset();
        }
    }
}
