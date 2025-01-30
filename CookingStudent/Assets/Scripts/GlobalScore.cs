using UnityEngine;

[CreateAssetMenu(fileName = "GlobalScore", menuName = "Scriptable Objects/GlobalScore")]
public class GlobalScore : ScriptableObject
{
    [SerializeField]
    private int playerScore;

    [SerializeField]
    private int burnCount;

    [SerializeField]
    private int trashCount;

    [SerializeField]
    private int contaminationCount;

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    public int GetScore()
    {

        int finalScore = 100 - playerScore;

        if (finalScore < 0)
        {
            finalScore = 0;
        }

        return finalScore;
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
