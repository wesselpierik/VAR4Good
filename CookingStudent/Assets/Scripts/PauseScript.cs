using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PauseScript : MonoBehaviour
{
    public GameObject wristUI;

    public bool activeWristUI = true;

    void Start()
    {
        DisplayWristUI();
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        DisplayWristUI();

    }
    public void DisplayWristUI()
    {
        if (activeWristUI)
        {
            wristUI.SetActive(false);
            activeWristUI = false;
            Time.timeScale = 1;
        }
        else if (!activeWristUI)
        {
            wristUI.SetActive(true);
            activeWristUI = true;
            Time.timeScale = 0;        
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
