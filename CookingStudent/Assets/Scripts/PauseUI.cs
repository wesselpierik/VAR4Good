using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PauseUI : MonoBehaviour
{
    public GameObject UI;
    public GameObject globalVolume;
    public InputActionReference openMenu;

    // public bool activeWristUI = false;

    public bool active = false;


    public PostcardUI postcardUI;

    void Start()
    {
        openMenu.action.started += PauseButtonPressed;
    }

    void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (active)
        {
            Deactivate();
            active = false;
        }
        else
        {
            Activate();
            active = true;
        }
    }

    void Activate()
    {
        if (UI == null || postcardUI == null || postcardUI.active) { return; }

        UI.SetActive(true);
        globalVolume.SetActive(true);
        Time.timeScale = 0;

        active = true;
    }

    void Deactivate()
    {
        UI.SetActive(false);
        globalVolume.SetActive(false);
        Time.timeScale = 1;

        active = false;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        /* If giving an error
        * Scene List:
        * 0: Main Menu
        * 1: Main Scene
        */
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
