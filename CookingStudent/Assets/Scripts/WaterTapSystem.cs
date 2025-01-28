using UnityEngine;
using UnityEngine.InputSystem;

public class WaterTapInteraction : MonoBehaviour
{
    public GameObject WaterStream;
    public InputActionReference TapButton;

    private int playerInReach = 0;
    private bool activeWaterTap = false;

    void Start() {
        if (WaterStream != null) {
            WaterStream.SetActive(false);
            TapButton.action.started += ToggleWaterStream;
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        Debug.Log(other.transform.tag);
        if (other.CompareTag("Hand")) {
            playerInReach++;
            TapButton.action.Enable();
        }
        Debug.Log($"Hand entered {playerInReach}");
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Hand")) {
            playerInReach--;
            TapButton.action.Disable();
        }
        Debug.Log($"Hand left {playerInReach}");
    }

    void ToggleWaterStream(InputAction.CallbackContext context) {
        Debug.Log($"Toggled {playerInReach}");
        if (playerInReach > 0 && WaterStream != null) {
            activeWaterTap = !activeWaterTap;
            WaterStream.SetActive(activeWaterTap);
        }
    }
}
