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
    public GameObject resultUI;

    public Camera cam;
    private Image fadeImage;

    public AudioSource musicSource;

    private Color fadeColor;

    public GameObject globalVolume;
    public InputActionReference openMenu;

    private bool postcardActive = false;
    private bool pauseActive = false;
    private bool fadeActive = true;
    private bool resultActive = false;


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
        // PauseUI(postcardActive);
        BlurUI(postcardActive);
    }

    void ToggleMenu()
    {
        pauseActive = !pauseActive;
        pauseUI.SetActive(pauseActive);
        PauseUI(pauseActive);
        BlurUI(pauseActive);
    }

    public void ToggleResult()
    {
        resultActive = !resultActive;
        resultUI.SetActive(resultActive);
        resultUI.GetComponent<FinalScoreUI>().Show();

        PauseUI(resultActive);
        BlurUI(resultActive);
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

    void BlurUI(bool active)
    {
        globalVolume.SetActive(active);
    }

    void PauseUI(bool active)
    {
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
            timer += Time.deltaTime;
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
        Time.timeScale = 1;
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
