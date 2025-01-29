using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject postcardUI;
    public GameObject pauseUI;
    public GameObject globalVolume;
    public InputActionReference openMenu;

    private bool postcardActive = false;
    private bool pauseActive = false;


    void Start()
    {
        openMenu.action.started += PauseButtonPressed;

        TogglePostcard();
    }

    void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (postcardActive)
        {
            TogglePostcard();
            return;
        }

        ToggleMenu();
    }

    void TogglePostcard()
    {
        postcardActive = !postcardActive;
        postcardUI.SetActive(postcardActive);
        ToggleUI(postcardActive);
    }

    void ToggleMenu()
    {
        pauseActive = !pauseActive;
        pauseUI.SetActive(pauseActive);
        ToggleUI(pauseActive);
    }

    void ToggleUI(bool active)
    {
        globalVolume.SetActive(active);
        Time.timeScale = active ? 0 : 1;
    }

    public void ContinueGame()
    {
        ToggleMenu();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
