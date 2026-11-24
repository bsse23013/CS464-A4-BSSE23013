using System;

// events with no particular sender, so the HUD can hear the player without a reference to it.
// They are static and outlive every scene, so whatever subscribes must unsubscribe in OnDisable.
public static class GameEvents
{
    public static event Action<int, int> PlayerDamaged;
    public static event Action<int, int> PlayerHealed;

    public static void RaisePlayerDamaged(int current, int max) => PlayerDamaged?.Invoke(current, max);
    public static void RaisePlayerHealed(int current, int max) => PlayerHealed?.Invoke(current, max);
}
