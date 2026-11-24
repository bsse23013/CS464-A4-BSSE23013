using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainPanel, selectPanel;
    [SerializeField] GameObject playButton, firstLevelButton;
    [SerializeField] Button[] levelButtons;      // one per level
    [SerializeField] TMP_Text[] levelLabels;

    void Start()
    {
        BuildLevelSelect();
        ShowMain();
    }

    // the level buttons are built from SaveData: locked until unlocked, best time if there is one
    void BuildLevelSelect()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i + 1;
            bool open = SaveData.IsUnlocked(level);
            levelButtons[i].interactable = open;
            if (!open) levelLabels[i].text = $"Level {level}  (locked)";
            else if (SaveData.HasBest(level)) levelLabels[i].text = $"Level {level}  best {SaveData.GetBest(level):0.0} s";
            else levelLabels[i].text = $"Level {level}";
        }
    }

    public void ShowMain()
    {
        selectPanel.SetActive(false);
        mainPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void ShowSelect()
    {
        mainPanel.SetActive(false);
        selectPanel.SetActive(true);
        BuildLevelSelect();
        EventSystem.current.SetSelectedGameObject(firstLevelButton);
    }

    public void Play() => LevelLoader.Instance.Load(1);

    public void PlayLevel(int level) => LevelLoader.Instance.Load(level);

    public void Credits() => LevelLoader.Instance.Load(6);

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
