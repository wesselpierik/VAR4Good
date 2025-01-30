using TMPro;
using UnityEngine;

public class FinalScoreUI : MonoBehaviour
{
    public TextMeshProUGUI contaminateText;
    public TextMeshProUGUI burnText;
    public TextMeshProUGUI trashText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI feedbackText;
    public GlobalScore globalScore;

    public void Show()
    {
        ContaminateText();
        BurnText();
        TrashText();
        FinalScore();
        Feedback();
    }

    private void ContaminateText()
    {
        int contaminateCount = globalScore.GetContaminationCount();
        contaminateText.text = $"You contaminated something {contaminateCount} times.";
    }

    private void BurnText()
    {
        int burnCount = globalScore.GetBurnCount();
        burnText.text = $"You have burned something {burnCount} times.";
    }

    private void TrashText()
    {
        int trashCount = globalScore.GetTrashCount();
        trashText.text = $"You have thrown something in the trash {trashCount} times.";
    }


    private void FinalScore()
    {
        int finalScore = globalScore.GetScore();

        finalScoreText.text = $"Final Score: {finalScore}";
    }

    private void Feedback()
    {
        int finalScore = globalScore.GetScore();

        if (finalScore > 70)
        {
            feedbackText.text = "You did well!";

        }
        else
        {
            feedbackText.text = "You should practice more...";
        }
    }

}
