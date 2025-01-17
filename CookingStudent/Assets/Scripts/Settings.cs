using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    [SerializeField]
    private bool showContamination;

    public void SetShowContamination(bool value) {
        showContamination = value;
    }

    public bool GetShowContamination() {
        return showContamination;
    }

    public void ResetShowContamination() {
        showContamination = true;
    }
}
