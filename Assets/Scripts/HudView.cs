using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// the parts of the HUD that GameManager writes to, so the prefab can be dropped into any level
public class HudView : MonoBehaviour
{
    public TMP_Text scoreText, livesText;
    [SerializeField] TMP_Text hintText;
    [SerializeField] GameObject hintPanel;
    [SerializeField] GameObject winPanel, losePanel;
    [SerializeField] GameObject winFirstButton, loseFirstButton;
    [SerializeField] TMP_Text winText;

    // the hint sits on its own panel, which is only there while it has something to say
    public void SetHint(string message)
    {
        hintText.text = message;
        hintPanel.SetActive(message != "");
    }

    public void ShowWin(string summary)
    {
        winText.text = summary;
        winPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(winFirstButton);
    }

    public void ShowLose()
    {
        losePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(loseFirstButton);
    }
}
