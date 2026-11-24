using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] float spinSpeed = 90f;
    [SerializeField] GameObject burst;      // a little particle pop
    [SerializeField] AudioClip pickup;

    void Update() => transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(value);
            SfxPlayer.Instance.Play(pickup);
            if (burst != null) Instantiate(burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
