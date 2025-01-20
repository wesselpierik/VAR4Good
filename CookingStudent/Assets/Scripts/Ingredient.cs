using UnityEngine;
using System;

[Serializable]
public class Ingredient
{
    public string ObjectName; // Name of the object to slice
    public int TargetCount;  // Number required
    public int CurrentCount; // Current sliced count
    public int TargetCookMinutes;
    public int CurrentCookMinutes;

    public void UpdateIngredientProgress(int action)
    {
        switch (action) {
            case 0: // Slice the ingredient
                CurrentCount++;
                break;

            case 1: // Cook the ingredient
                CurrentCookMinutes++;
                break;

        }

        Debug.Log($"{ObjectName}: {CurrentCount}/{TargetCount}");
    }

    public bool IsIngredientComplete()
    {
        // Check if all recipeingrdients have exceeded the goal.
        return CurrentCount >= TargetCount && CurrentCookMinutes >= TargetCookMinutes;
    }
}
