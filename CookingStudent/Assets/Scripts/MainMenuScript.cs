using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Toggle contaminationToggle;
    public void StartGame()
    {
        // SceneManager.LoadScene("Main Scene");
        Debug.Log("Start");


    }

    public void Toggle()
    {
        if (contaminationToggle.isOn)
        {
            Debug.Log("Contamination enabled.");
        }
        else
        {
            Debug.Log("Contamination disabled.");
        }
    }

    public void Exit()
    {
        // Application.Quit();
        Debug.Log("Quit");
    }
}
