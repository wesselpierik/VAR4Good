using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    public AudioResource[] resources;
    [Range(0f, 1f)] public float[] resourceVolumes;

    // public AudioClip[] clips;

    // [Range(0f, 1f)] public float[] clipVolumes;

    private AudioSource audioSource;

    public float delay = 0f; //delay in seconds
    private float timer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found!");
        }

        if (resources.Length != resourceVolumes.Length)
        {
            Debug.LogError("Clips and Clip Volumes should be the same length!");
        }
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Play(int resourceNumber = 0)
    {
        if (timer <= 0f)
        {
            if (resources.Length != 0)
            {
                audioSource.resource = resources[resourceNumber];
                audioSource.volume = resourceVolumes[resourceNumber];
            }

            timer = delay;
            audioSource.Play();
        }
    }

    public void PlayOneShot(int resourceNumber = 0)
    {
        if (timer <= 0f)
        {
            timer = delay;
            audioSource.PlayOneShot(resources[resourceNumber] as AudioClip, resourceVolumes[resourceNumber]);
        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
