using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject postcardUI;
    public GameObject pauseUI;
    public GameObject globalVolume;
    public InputActionReference openMenu;

    public bool postcardActive = false;
    private bool pauseActive = false;


    void Start()
    {
        openMenu.action.started += PauseButtonPressed;

        if (postcardActive)
        {
            postcardUI.SetActive(true);
            Pause();
            return;
        }
    }

    void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (postcardActive)
        {
            postcardActive = false;
            postcardUI.SetActive(false);
            Unpause();
            return;
        }

        if (pauseActive)
        {
            pauseActive = false;
            pauseUI.SetActive(false);
            Unpause();
        }
        else
        {
            pauseActive = true;
            pauseUI.SetActive(true);
            Pause();
        }
    }

    void Pause()
    {
        globalVolume.SetActive(true);
        Time.timeScale = 0;
    }

    void Unpause()
    {
        globalVolume.SetActive(false);
        Time.timeScale = 1;
    }

}
