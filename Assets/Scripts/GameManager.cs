using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int targetScore = 8, lives = 3;
    [SerializeField] float respawnDelay = 0.6f, spawnProtection = 1.5f;
    [SerializeField] Health player;
    [SerializeField] Transform respawnPoint;
    [SerializeField] AudioClip winClip, loseClip;

    HudView hud;
    int score;

    public bool Over { get; private set; }

    void Awake()
    {
        Instance = this;
        hud = FindAnyObjectByType<HudView>();
    }

    // listener 5: the lives and hearts text follow the health events
    void OnEnable() => GameEvents.PlayerDamaged += OnHealthChanged;
    void OnDisable() => GameEvents.PlayerDamaged -= OnHealthChanged;

    void OnHealthChanged(int current, int max) => UpdateUI();

    void Start()
    {
        hud.SetHint("");
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        hud.scoreText.text = $"Coins: {score} / {targetScore}";
        hud.livesText.text = $"Lives: {lives}";
    }

    // the goal only counts once enough coins are in
    public void ReachGoal()
    {
        if (Over) return;
        if (score >= targetScore) Win();
        else StartCoroutine(Hint($"Collect {targetScore - score} more coin(s) first"));
    }

    IEnumerator Hint(string message)
    {
        hud.SetHint(message);
        yield return new WaitForSeconds(2f);
        hud.SetHint("");
    }

    public void SetCheckpoint(Transform point) => respawnPoint = point;

    public void PlayerDied()
    {
        lives--;
        UpdateUI();
        PersistentMusic.Pause();   // the music drops out when the player dies or falls
        if (lives <= 0) Lose();
        else StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        Rigidbody body = player.GetComponent<Rigidbody>();
        body.position = respawnPoint.position;
        body.linearVelocity = Vector3.zero;
        player.Restore(spawnProtection);
        PersistentMusic.Resume();
    }

    void Win()
    {
        Over = true;
        int level = gameObject.scene.buildIndex;
        float time = Time.timeSinceLevelLoad;
        bool best = SaveData.Record(level, time);
        SaveData.Unlock(level + 1);
        PersistentMusic.Stop();
        SfxPlayer.Instance.Play(winClip);
        hud.ShowWin($"Level {level} complete!\nTime {time:0.0} s" + (best ? "  (new best!)" : $"  (best {SaveData.GetBest(level):0.0} s)"));
        Time.timeScale = 0f;
    }

    void Lose()
    {
        Over = true;
        PersistentMusic.Stop();
        SfxPlayer.Instance.Play(loseClip);
        hud.ShowLose();
        Time.timeScale = 0f;
    }
}
