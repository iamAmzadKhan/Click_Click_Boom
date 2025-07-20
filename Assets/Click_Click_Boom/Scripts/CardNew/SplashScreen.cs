using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadMainMenu", 3f);
    }

    void LoadMainMenu()
    {
        UI_Manager.Instance.ShowScreen("MainMenu");
    }
}
