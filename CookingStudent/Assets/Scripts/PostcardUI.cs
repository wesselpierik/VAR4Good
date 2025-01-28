using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;

public class PostcardUI : MonoBehaviour
{
    public GameObject UI;
    public GameObject globalVolume;
    public InputActionReference openMenu;

    public bool active = false;

    void Start()
    {
        openMenu.action.started += PauseButtonPressed;
        Time.timeScale = 0;
    }

    void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (active || UI == null) return;

        UI.SetActive(false);
        globalVolume.SetActive(false);
        Time.timeScale = 1;
    }
}
