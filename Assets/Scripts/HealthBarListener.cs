using UnityEngine;

// listener 1: the health bar answers every hit and every heal
public class HealthBarListener : MonoBehaviour
{
    [SerializeField] HealthBar bar;

    void OnEnable()
    {
        GameEvents.PlayerDamaged += OnChanged;
        GameEvents.PlayerHealed += OnChanged;
    }

    void OnDisable()
    {
        GameEvents.PlayerDamaged -= OnChanged;
        GameEvents.PlayerHealed -= OnChanged;
    }

    void OnChanged(int current, int max) => bar.Set(current, max);
}
