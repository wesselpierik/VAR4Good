using UnityEngine;

[CreateAssetMenu(fileName = "GlobalScore", menuName = "Scriptable Objects/GlobalScore")]
public class GlobalScore : ScriptableObject
{
    private int playerScore = 109; // +9 for initial contamination
    private int burnCount;
    private int trashCount;
    private int contaminationCount;

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    public int GetScore()
    {
        // if (playerScore < 0)
        // {
        //     playerScore = 0;
        // }

        return playerScore;
    }

    public void ResetScore()
    {
        playerScore = 0;
    }

    public void TrashCount()
    {
        trashCount++;
    }
    
    public int GetTrashCount()
    {
        return trashCount;
    }

    public void ContaminationCount()
    {
        contaminationCount++;
    }

    public int GetContaminationCount()
    {
        return contaminationCount;
    }

    public void BurnCount()
    {
        burnCount++;
    }

    public int GetBurnCount()
    {
        return burnCount;
    }
}
