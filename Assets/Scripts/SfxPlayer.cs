using UnityEngine;

// every sound effect goes through here: PlayOneShot with a random pitch
public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer Instance { get; private set; }

    [SerializeField] AudioSource[] sources;
    [SerializeField] float pitchRange = 2f;      // +- semitones
    [SerializeField] float minVolume = 0.85f;
    int next;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);           // a reload doesn't cut off the sound that caused it
        foreach (AudioSource s in sources) s.ignoreListenerPause = true;   // button clicks still work while paused
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        AudioSource source = sources[next];
        next = (next + 1) % sources.Length;
        source.pitch = Mathf.Pow(2f, Random.Range(-pitchRange, pitchRange) / 12f);
        source.PlayOneShot(clip, volume * Random.Range(minVolume, 1f));
    }
}
