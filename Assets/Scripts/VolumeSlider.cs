using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// a volume slider for one mixer group: the slider is 0.0001 to 1, the mixer wants decibels
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] string parameter = "MusicVolume";   // the group's Volume, exposed and renamed
    [SerializeField] Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(parameter, 1f);
        slider.onValueChanged.AddListener(Set);
        Set(slider.value);
    }

    void Set(float value)
    {
        mixer.SetFloat(parameter, Mathf.Log10(value) * 20f);   // 1 = 0 dB, 0.5 = -6 dB, 0.0001 = -80 dB
        PlayerPrefs.SetFloat(parameter, value);
    }
}
