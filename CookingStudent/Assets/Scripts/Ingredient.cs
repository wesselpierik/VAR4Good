using UnityEngine;
using System;


[Serializable]
public class Ingredient
{
    public string ObjectName; // Name of the object to slice
    public int TargetCount;  // Number required
    public int CurrentCount; // Current sliced count
    public float BurnCookMinutes;
    public float TargetCookMinutes;
    public float CurrentCookMinutes;

    public string UpdateIngredientProgress(int action, float timer)
    {
        switch (action)
        {
            case 0: // Slice the ingredient
                CurrentCount++;
                break;

            case 1: // Cook the ingredient
                CurrentCookMinutes = Math.Max(timer, CurrentCookMinutes);
                break;

        }

        string recipeStatus = $"{ObjectName}: {CurrentCount}/{TargetCount}";

        return recipeStatus;
    }

    public bool IsIngredientComplete()
    {
        // Check if all recipeingrdients have exceeded the goal.
        return CurrentCount >= TargetCount && CurrentCookMinutes >= TargetCookMinutes;
    }
}
