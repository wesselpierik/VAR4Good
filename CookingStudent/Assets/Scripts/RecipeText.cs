using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class RecipeText : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        RecipeTextAdd();
    }

    public void RecipeTextAdd()
    {
        List<Ingredient> ingredients = GlobalStateManager.Instance.recipeList;
        text.text = "";

        foreach (Ingredient ingredient in ingredients)
        {
            string name = ingredient.ObjectName;
            int currentCount = ingredient.CurrentCount;
            int targetCount = ingredient.TargetCount;
            int currentCookMinutes = ingredient.CurrentCookMinutes;
            int targetCookMinutes = ingredient.TargetCookMinutes;

            string ingredientLine = "";

            if (targetCookMinutes > 0)
            {
                ingredientLine += $"Cook the {name}";
            }
            else
            {
                ingredientLine += $"Slice the {name}: {currentCount}/{targetCount}";
            }


            if (currentCount >= targetCount && currentCookMinutes >= targetCookMinutes)
            {
                ingredientLine = "<color=green>" + ingredientLine + "</color>";
            }

            text.text += ingredientLine;
            text.text += "\n";
        }

    }


}
