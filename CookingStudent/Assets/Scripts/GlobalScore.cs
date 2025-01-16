using UnityEngine;

[CreateAssetMenu(fileName = "GlobalScore", menuName = "Scriptable Objects/GlobalScore")]
public class GlobalScore : ScriptableObject
{
    [SerializeField]
    private int playerScore;

    public void AddScore(int amount) {
        playerScore += amount;
    }

    public int GetScore() {
        return playerScore;
    }

    public void ResetScore() {
        playerScore = 0;
    }
}
