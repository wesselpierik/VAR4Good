// THIS FILE IS NOT USED ANYMORE. IT IS ONLY USED IN THE RECIPE SYSTEM SCENE
// AND SHOULD BE REMOVED WHEN THAT SCENE IS DELETED.

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


[Serializable]
public class Recipe
{
    public string ObjectName; // Name of the object to slice
    public int TargetCount;  // Number required
    public int CurrentCount; // Current sliced count
}

public class RecipeList : MonoBehaviour
{
    public List<Recipe> Recipes = new List<Recipe>();
    public event Action<Recipe> OnRecipeEvent;

    public void UpdateRecipeProgress(string objectName)
    {   
        // Match an recipeingredient to one from the list.
        Recipe recipe = Recipes.FirstOrDefault(r => r.ObjectName == objectName);
        if (recipe != null)
        {
            recipe.CurrentCount++;
            // Check if there is an event, if so, invoke it with recipe as argument.
            OnRecipeEvent?.Invoke(recipe);
        }
    }

    public bool IsRecipeComplete()
    {
        // Check if all recipeingrdients have exceeded the goal.
        return Recipes.All(r => r.CurrentCount >= r.TargetCount);
    }
}
