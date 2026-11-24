using UnityEngine;
using UnityEngine.EventSystems;

// on the HUD canvas: pause, resume, restart and the other menu buttons
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject firstButton;
    public static bool IsPaused { get; private set; }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.Over) return;   // no pausing on an end screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        IsPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void Resume()
    {
        IsPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    // every load comes after Resume(), or the next scene starts frozen or silent
    public void Restart()
    {
        Resume();
        LevelLoader.Instance.Reload();
    }

    public void NextLevel()
    {
        Resume();
        LevelLoader.Instance.LoadNext();
    }

    public void QuitToMenu()
    {
        Resume();
        LevelLoader.Instance.LoadMenu();
    }
}
