using UnityEngine;

public class GlobalSettingsManager : MonoBehaviour
{
    public Settings settings;

    private static GlobalSettingsManager _instance;

    public static GlobalSettingsManager Instance {
        get {
            if (_instance == null) {
                _instance = FindAnyObjectByType<GlobalSettingsManager>();
                if (_instance == null) {
                    Debug.LogError("There is no GlobalSettingsManager found");
                }
            }

            return _instance;
        }
    }

    private void Start()
    {
        if (settings == null) {
            Debug.LogWarning("settings is null");
        }
    }

    public void SetShowContamination(bool value) {
        if (settings == null) {
            Debug.LogWarning("settings is null");
        }
        settings.SetShowContamination(value);
    }

    public void DisplaySettings() {
        Debug.Log(settings.GetShowContamination());
    }
}
