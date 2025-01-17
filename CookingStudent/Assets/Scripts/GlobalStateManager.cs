using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    public GlobalScore globalScore;
    public GlobalRecipe globalRecipe;

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

        if (globalRecipe == null) {
            Debug.LogWarning("globalRecipe is null");
        }
        globalRecipe.ResetRecipe();
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

    public int GetCutsState()
    {
        return globalRecipe.GetCutsState();
    }

    public int GetCookingState()
    {
        return globalRecipe.GetCookingState();
    }

   public void UpdateCutsState()
    {
        globalRecipe.UpdateCutsState();
    }
}
