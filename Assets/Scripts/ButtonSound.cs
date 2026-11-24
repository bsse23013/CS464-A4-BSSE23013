using UnityEngine;
using UnityEngine.UI;

// plays the menu click through the SfxPlayer
[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip click;

    void Start() => GetComponent<Button>().onClick.AddListener(() => SfxPlayer.Instance.Play(click));
}
