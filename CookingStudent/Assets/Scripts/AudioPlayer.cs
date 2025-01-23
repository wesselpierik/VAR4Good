using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    public float delay = 0.6f; //delay in seconds
    private float timer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found!");
        }
    }

    public void Play()
    {
        if (timer <= 0f)
        {
            audioSource.Play();
            timer = delay;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
