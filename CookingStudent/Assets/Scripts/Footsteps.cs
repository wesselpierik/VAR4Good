using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioPlayer audioPlayer;
    private Vector3 lastPosition;

    private CharacterController controller;

    void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();
        controller = GetComponent<CharacterController>();

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer not found!");
        }

        if (controller == null)
        {
            Debug.LogError("CharacterController not found!");
        }

        // prevent audio from playing at the start
        lastPosition = controller.center;
    }

    void Update()
    {
        Vector3 currentPosition = controller.center;
        if (currentPosition != lastPosition)
        {
            audioPlayer.Play();
            lastPosition = currentPosition;
        }
    }
}
