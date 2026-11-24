using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// one music player for the whole game, so the music carries on through every load
public class PersistentMusic : MonoBehaviour
{
    static PersistentMusic instance;
    [SerializeField] float fadeTime = 1f;
    AudioSource source;
    Coroutine fade;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        // keep playing under the pause menu so the slider can be heard
        source.ignoreListenerPause = true;
        source.volume = 0f;          // fade in at the very start too
        FadeTo(1f, false, false);
    }

    void OnEnable() => SceneManager.sceneLoaded += OnLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnLoaded;

    // a level that was stopped at its end starts the music again in the next scene
    void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        if (instance != this) return;
        if (!source.isPlaying) source.Play();
        FadeTo(1f, false, false);
    }

    // fade the AudioSource volume (the slider is on the mixer, so they don't fight)
    void FadeTo(float target, bool pauseAtEnd, bool stopAtEnd)
    {
        if (fade != null) StopCoroutine(fade);
        fade = StartCoroutine(Fade(target, pauseAtEnd, stopAtEnd));
    }

    IEnumerator Fade(float target, bool pauseAtEnd, bool stopAtEnd)
    {
        float start = source.volume;
        for (float t = 0f; t < fadeTime; t += Time.unscaledDeltaTime)
        {
            source.volume = Mathf.Lerp(start, target, t / fadeTime);
            yield return null;
        }
        source.volume = target;
        if (pauseAtEnd) source.Pause();
        if (stopAtEnd) source.Stop();
    }

    public static void Stop() { if (instance != null) instance.FadeTo(0f, false, true); }
    public static void Pause() { if (instance != null) instance.FadeTo(0f, true, false); }

    public static void Resume()
    {
        if (instance == null) return;
        instance.source.UnPause();
        instance.FadeTo(1f, false, false);
    }
}
