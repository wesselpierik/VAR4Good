using UnityEngine;

public class Collision : MonoBehaviour
{
    private AudioPlayer audioPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer not found!");
        }
    }

    void OnCollisionEnter()
    {
        audioPlayer.Play();
    }
}
