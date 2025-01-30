using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    public AudioResource[] resources;
    [Range(0f, 1f)] public float[] resourceVolumes;

    // public AudioClip[] clips;

    // [Range(0f, 1f)] public float[] clipVolumes;

    private AudioSource audioSource;
    private AudioSource audioSource2;


    public float delay = 0f; //delay in seconds
    private float timer = 0f;

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length > 0)
        {
            audioSource = audioSources[0];
        }
        if (audioSources.Length > 1)
        {
            audioSource2 = audioSources[1];
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

    public void Play2(int resourceNumber = 0)
    {
        if (timer <= 0f)
        {
            if (resources.Length != 0)
            {
                audioSource2.resource = resources[resourceNumber];
                audioSource2.volume = resourceVolumes[resourceNumber];
            }

            timer = delay;
            audioSource2.Play();
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
        if (audioSource2 != null)
        {
            audioSource2.Stop();
        }
    }
}
