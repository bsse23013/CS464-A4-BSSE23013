using UnityEngine;

// the credits only has a button that leads back: LoadNext wraps to the menu after the last scene
public class CreditsScreen : MonoBehaviour
{
    public void Back() => LevelLoader.Instance.LoadNext();
}
