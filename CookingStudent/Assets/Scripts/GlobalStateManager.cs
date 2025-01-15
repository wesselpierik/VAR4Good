using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    public GlobalScore globalScore;

    private static GlobalStateManager _instance;

    public static GlobalStateManager Instance {
        get {
            if (_instance == null) {
                _instance = FindAnyObjectByType<GlobalStateManager>();
                if (_instance == null) {
                    Debug.LogError("There is no GlobalStateManager found");
                }
            }

            return _instance;
        }
    }

    private void Start()
    {
        if (globalScore == null) {
            Debug.LogWarning("globalScore is null");
        }
        globalScore.ResetScore();
    }

    public void AddScore(int amount) {
        if (globalScore == null) {
            Debug.LogWarning("globalScore is null");
        }
        globalScore.AddScore(amount);
    }

    public void DisplayScore() {
        Debug.Log(globalScore.GetScore());
    }
}
