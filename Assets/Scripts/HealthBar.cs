using UnityEngine;
using UnityEngine.UI;

// two filled Images: the fill in front and a paler lag bar behind it
public class HealthBar : MonoBehaviour
{
    [SerializeField] Image fill;             // Filled, Horizontal, Origin Left
    [SerializeField] Image lag;              // the same, behind it, in a paler colour
    [SerializeField] Color lowColour, midColour, highColour;   // the fill goes low, mid, high as health rises
    [SerializeField] float lagDelay = 0.4f;  // seconds before the lag bar starts to catch up
    [SerializeField] float lagSpeed = 0.8f;  // fill per second it catches up at
    float target = 1f;
    float lagStartsAt;

    public void Set(int current, int max)
    {
        float next = Mathf.Clamp01((float)current / max);
        if (next < target) lagStartsAt = Time.unscaledTime + lagDelay;
        else lag.fillAmount = next;          // healing: the lag bar moves up with it
        target = next;
        fill.fillAmount = target;
        fill.color = target < 0.5f ? Color.Lerp(lowColour, midColour, target * 2f)
                                   : Color.Lerp(midColour, highColour, (target - 0.5f) * 2f);
    }

    void Update()
    {
        if (Time.unscaledTime < lagStartsAt) return;
        lag.fillAmount = Mathf.MoveTowards(lag.fillAmount, target, lagSpeed * Time.unscaledDeltaTime);
    }
}
