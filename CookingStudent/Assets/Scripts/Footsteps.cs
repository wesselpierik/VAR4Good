using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioPlayer audioPlayer;
    private Vector3 lastPosition;

    void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer not found!");
        }

        // prevent audio from playing at the start
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        if (currentPosition != lastPosition)
        {
            audioPlayer.Play();
            lastPosition = currentPosition;
        }
    }
}
