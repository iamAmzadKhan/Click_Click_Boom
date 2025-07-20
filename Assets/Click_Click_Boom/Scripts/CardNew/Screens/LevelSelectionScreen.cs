using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionScreen : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent;

    public void Start()
    {
        CreateLevelButtons();
    }

    private void CreateLevelButtons()
    {
        string[] levels = { "2x2", "2x3", "2x4", "2x5", "2x6", "3x2", "3x4"};

        foreach (var level in levels)
        {
            string[] dimensions = level.Split('x');
            int rows = int.Parse(dimensions[0]);
            int columns = int.Parse(dimensions[1]);

            GameObject buttonObj = Instantiate(buttonPrefab, buttonParent);
            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();
            Button button = buttonObj.GetComponent<Button>();
            button.gameObject.SetActive(true);
            buttonText.text = level;

            button.onClick.AddListener(() => OnLevelButtonClicked(rows, columns));
        }
    }

    private void OnLevelButtonClicked(int rows, int columns)
    {
        UI_Manager.Instance.ShowScreen("Game");
        Deck_Manager.Instance.CreateDeck(rows, columns);
    }
}
