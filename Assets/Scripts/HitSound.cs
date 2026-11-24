using UnityEngine;

// listener 3: the hurt sound, on the player
public class HitSound : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] AudioClip hit;

    void OnEnable() => health.Damaged += OnHit;
    void OnDisable() => health.Damaged -= OnHit;

    void OnHit(int current, int max) => SfxPlayer.Instance.Play(hit);
}
