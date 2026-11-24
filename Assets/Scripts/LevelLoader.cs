using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// the only script that loads a scene, behind a loading bar
public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    [SerializeField] GameObject barRoot;
    [SerializeField] Image barFill;
    [SerializeField] int firstSceneIndex = 0;   // the menu, where we wrap to after Credits

    bool loading;

    void Awake() => Instance = this;

    void Start()
    {
        if (barRoot != null) barRoot.SetActive(false);
    }

    public void Load(int index)
    {
        if (loading) return;
        loading = true;
        Time.timeScale = 1f;    // it survives a scene load
        StartCoroutine(LoadRoutine(index));
    }

    public void LoadNext()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next >= SceneManager.sceneCountInBuildSettings) next = firstSceneIndex;
        Load(next);
    }

    public void Reload() => Load(SceneManager.GetActiveScene().buildIndex);

    public void LoadMenu() => Load(firstSceneIndex);

    IEnumerator LoadRoutine(int index)
    {
        barRoot.SetActive(true);
        AsyncOperation op = SceneManager.LoadSceneAsync(index);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            barFill.fillAmount = Mathf.Clamp01(op.progress / 0.9f);
            yield return null;
        }
        barFill.fillAmount = 1f;
        yield return new WaitForSecondsRealtime(0.2f);   // let the full bar be seen
        op.allowSceneActivation = true;
    }
}
