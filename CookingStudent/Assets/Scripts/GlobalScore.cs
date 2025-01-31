using UnityEngine;

[CreateAssetMenu(fileName = "GlobalScore", menuName = "Scriptable Objects/GlobalScore")]
public class GlobalScore : ScriptableObject
{
    private int playerScore;
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
        playerScore = 115;  // +15 for initial contamination
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
