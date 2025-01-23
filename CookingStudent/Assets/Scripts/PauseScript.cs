using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PauseScript : MonoBehaviour
{
    public GameObject wristUI;
    public GameObject globalVolume;

    public bool activeWristUI = false;

    void Start() {
        DisplayWristUI();
    }

    public void PauseButtonPressed(InputAction.CallbackContext context) {
        if (context.performed) {
            DisplayWristUI();
        }
    }

    public void DisplayWristUI() {
        activeWristUI = !activeWristUI;
        wristUI.SetActive(activeWristUI);
        globalVolume.SetActive(activeWristUI);
        Time.timeScale = activeWristUI ? 0 : 1;

    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
