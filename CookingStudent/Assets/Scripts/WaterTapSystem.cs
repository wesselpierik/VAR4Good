using UnityEngine;
using UnityEngine.InputSystem;

public class WaterTapInteraction : MonoBehaviour
{
    public GameObject WaterStream; 
    public InputActionReference TapButton; 
    private bool PlayerInReach = false; 

    void Start()
    {
        if (WaterStream != null)
        {
            WaterStream.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerInReach = true;
        TapButton.action.Enable();
    }

    void OnTriggerExit(Collider other)
    {
        PlayerInReach = false;
        TapButton.action.Disable();
    }

    void Update()
    {
        if (PlayerInReach && TapButton.action.triggered) 
        {
            if (WaterStream != null)
            {
                bool isActief = WaterStream.activeSelf;
                WaterStream.SetActive(!isActief);
            }
        }
    }
}
