using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Toggle contaminationToggle;
    public GameObject globalVolume;

    void Start()
    {
        globalVolume.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("Main Scene");
    }

    public void Toggle()
    {
        if (contaminationToggle.isOn)
        {
            Debug.Log("Contamination enabled.");
            GlobalSettingsManager.Instance.SetShowContamination(true);
        }
        else
        {
            Debug.Log("Contamination disabled.");
            GlobalSettingsManager.Instance.SetShowContamination(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
