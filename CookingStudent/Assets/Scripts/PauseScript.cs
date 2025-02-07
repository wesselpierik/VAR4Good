using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PauseScript : MonoBehaviour
{
    public GameObject wristUI;
    public GameObject globalVolume;
    public InputActionReference openMenu;

    public bool activeWristUI = false;

    void Start()
    {
        openMenu.action.started += PauseButtonPressed;
    }

    void PauseButtonPressed(InputAction.CallbackContext context)
    {
        DisplayWristUI();
    }

    public void DisplayWristUI()
    {
        activeWristUI = !activeWristUI;
        if (wristUI == null) { return; }
        wristUI.SetActive(activeWristUI);
        globalVolume.SetActive(activeWristUI);
        Time.timeScale = activeWristUI ? 0 : 1;
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
