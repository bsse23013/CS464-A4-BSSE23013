using UnityEngine;

// a looping sound that has a place in the level: set up the 3D settings from the Inspector
[RequireComponent(typeof(AudioSource))]
public class WorldSound : MonoBehaviour
{
    [SerializeField] float minDistance = 2f;    // as loud as it gets inside this
    [SerializeField] float maxDistance = 25f;   // where the fade stops
    [SerializeField] AudioRolloffMode rolloff = AudioRolloffMode.Logarithmic;

    void Awake()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.spatialBlend = 1f;               // fully 3D
        source.rolloffMode = rolloff;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.loop = true;
        if (!source.isPlaying) source.Play();
    }
}
