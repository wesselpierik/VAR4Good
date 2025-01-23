using UnityEngine;
using UnityEngine.TextCore.Text;

public class AudioFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    private Vector3 lastPosition;

    private CharacterController controller;

    public float delay = 0.6f; //delay in seconds
    private float timer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found!");
        }

        if (controller == null)
        {
            Debug.LogError("CharacterController not found!");
        }
    }

    void Update()
    {
        // if timer expired and the player moved
        if (timer <= 0f)
        {
            Vector3 currentPosition = controller.center;
            if (currentPosition != lastPosition)
            {
                // reset timer
                timer = delay;
                audioSource.Play();
                lastPosition = currentPosition;
            }

        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
