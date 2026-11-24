using UnityEngine;

// the follow camera is several metres from the player, so the ears sit on the player instead
public class ListenerOnPlayer : MonoBehaviour
{
    Transform player;
    Transform cam;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>().transform;
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.position = player.position;
        transform.rotation = cam.rotation;      // left and right match what is on screen
    }
}
