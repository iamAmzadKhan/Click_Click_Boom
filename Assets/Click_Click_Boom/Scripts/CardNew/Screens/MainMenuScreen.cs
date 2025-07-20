using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    public Button m_PlayButton;
    public Button m_CustomLevelButton;
    // Start is called before the first frame update
    void Start()
    {
        if(m_PlayButton != null)
        {
            m_PlayButton.onClick.AddListener(OnPlay);
        }

        if (m_CustomLevelButton != null)
        {
            m_CustomLevelButton.onClick.AddListener(OnCustomLevel);
        }

    }

    void OnPlay()
    {
        if(UI_Manager.Instance != null)
        {
            UI_Manager.Instance.ShowScreen("LevelSelection");
        }
    }

    void OnCustomLevel()
    {
        if (UI_Manager.Instance != null)
        {
            UI_Manager.Instance.ShowScreen("CustomLevel");
        }
    }

}
