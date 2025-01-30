using UnityEngine;
using UnityEngine.InputSystem;

public class WaterTapInteraction : MonoBehaviour
{
    public GameObject WaterStream;
    public GameObject WaterPool;

    public InputActionReference TapButton;

    public AudioPlayer audioPlayer;


    private int playerInReach = 0;
    private bool activeWaterTap = false;

    void Start()
    {
        if (WaterStream == null || WaterPool == null)
        {
            Debug.LogError("WaterStream or WaterPool not found!");
        }

        WaterStream.SetActive(false);
        WaterPool.SetActive(false);

        TapButton.action.performed += ToggleWaterStream;

        audioPlayer = GetComponent<AudioPlayer>();

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer not found!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other);
        // Debug.Log(other.transform.tag);
        if (other.CompareTag("Hand"))
        {
            playerInReach++;
            TapButton.action.Enable();
        }
        // Debug.Log($"Hand entered {playerInReach}");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            playerInReach--;
            TapButton.action.Disable();
        }
        // Debug.Log($"Hand left {playerInReach}");
    }

    void ToggleWaterStream(InputAction.CallbackContext context)
    {
        // Debug.Log($"Toggled {playerInReach}");
        if (playerInReach > 0 && WaterStream != null)
        {
            activeWaterTap = !activeWaterTap;
            WaterStream.SetActive(activeWaterTap);
            WaterPool.SetActive(activeWaterTap);


            if (activeWaterTap)
            {
                audioPlayer.Play(0);
                audioPlayer.Play2(1);
            }
            else
            {
                audioPlayer.Stop();
                audioPlayer.Play2(1);
            }
        }
    }
}
