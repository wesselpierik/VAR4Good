using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class UIManager : MonoBehaviour
{
    public GameObject postcardUI;
    public GameObject pauseUI;

    public GameObject fadeUI;
    public Camera cam;
    private Image fadeImage;

    public AudioSource musicSource;

    private Color fadeColor;


    public GameObject globalVolume;
    public InputActionReference openMenu;

    private bool postcardActive = false;
    private bool pauseActive = false;

    private bool fadeActive = true;

    public float fadeDuration = 2.0f;



    void Start()
    {
        openMenu.action.started += PauseButtonPressed;

        // fade
        fadeUI.SetActive(true);
        fadeImage = fadeUI.GetComponentInChildren<Image>();
        fadeColor = fadeImage.color;
        ToggleFade();

        // postcard
        TogglePostcard();
    }

    void PauseButtonPressed(InputAction.CallbackContext context)
    {
        // if postcard is active, close it and ignore the rest
        if (postcardActive)
        {
            TogglePostcard();
            if (musicSource != null) musicSource.Play();
            return;
        }

        ToggleMenu();
    }

    void TogglePostcard()
    {
        postcardActive = !postcardActive;
        postcardUI.SetActive(postcardActive);
        ShowHands(!postcardActive);
    }

    void ToggleMenu()
    {
        pauseActive = !pauseActive;
        pauseUI.SetActive(pauseActive);
        PauseUI(pauseActive);
    }

    void ToggleFade()
    {
        if (fadeActive)
        {
            FadeIn();
            fadeActive = false;
        }
        else
        {
            FadeOut();
            fadeActive = true;
        }
    }

    void PauseUI(bool active)
    {
        globalVolume.SetActive(active);
        Time.timeScale = active ? 0 : 1;
    }

    void ShowHands(bool active)
    {
        if (active)
        {
            cam.cullingMask = 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("Hands");
        }
        else
        {
            cam.cullingMask = 1 << LayerMask.NameToLayer("UI");
        }
    }


    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;

        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            fadeImage.color = newColor;
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        fadeImage.color = newColor2;
    }

    /* Public functions */
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
