using UnityEngine;
using UnityEngine.InputSystem;

public class WaterTapInteraction : MonoBehaviour
{
    public GameObject WaterStream; 
    public InputActionReference TapButton; 
    private bool playerInReach = false;
    private bool activeWaterTap = false;

    void Start() {
        if (WaterStream != null) {
            WaterStream.SetActive(false);
            TapButton.action.started += ToggleWaterStream;
        }
    }

    void OnTriggerEnter(Collider other) {
        playerInReach = true;
        TapButton.action.Enable();
    }

    void OnTriggerExit(Collider other) {
        playerInReach = false;
        TapButton.action.Disable();
    }

    void ToggleWaterStream(InputAction.CallbackContext context) {
        if (playerInReach && WaterStream != null) {
            activeWaterTap = !activeWaterTap;
            WaterStream.SetActive(activeWaterTap);
        }
    }
}
