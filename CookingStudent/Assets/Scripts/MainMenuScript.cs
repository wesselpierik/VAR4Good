using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class MainMenuScript : MonoBehaviour
{
    public Toggle contaminationToggle;
    public LocomotionProvider move;
    public LocomotionProvider turn;



    void Start()
    {
        move.enabled = false;
        turn.enabled = false;
    }

    public void StartGame()
    {
        Debug.Log("Start");
        move.enabled = true;
        turn.enabled = true;

        // SceneManager.LoadScene("Main Scene");
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
