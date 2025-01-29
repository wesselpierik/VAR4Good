using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEditor;


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

            
            //float currentCookMinutes = ingredient.CurrentCookMinutes;
            float targetCookMinutes = ingredient.TargetCookMinutes;
            //float burnCookminutes = ingredient.BurnCookMinutes;

            Debug.Log($"target cook minutes {targetCookMinutes}");
            Debug.Log($"slice current count {currentCount}");

            string ingredientLine = "";

            string[] ingNameArray = name.Split('_');
            int index = 2;
            string ingName = ingNameArray[index];
            Debug.Log(ingName);

            if (targetCookMinutes > 0)
            {

                GameObject obj = GameObject.Find(name);

                bool isDone = obj.GetComponent<IngredientCooking>().isDone;
                bool isBurnt = obj.GetComponent<IngredientCooking>().isBurnt;

                Debug.Log($"is done {isDone}");
                Debug.Log($"is burnt {isBurnt}");

                ingredientLine += $"Cook the {ingName}";

                if (isDone && !isBurnt)
                {
                    ingredientLine = "<color=green>" + ingredientLine + "</color>";
                }
            }

            if(targetCount > 0)
            {
                ingredientLine += $"Slice the {ingName}: {currentCount}/{targetCount}";

                if (currentCount >= targetCount)
                {
                    ingredientLine = "<color=green>" + ingredientLine + "</color>";
                }
            }

            text.text += ingredientLine;

            if (targetCookMinutes > 0 || targetCount > 0)
            {
                text.text += "\n";
            }

            bool complete = GlobalStateManager.Instance.isRecipeComplete();

        if (complete)
            {
                text.text = "Recipe complete! \n Please put all the ingredients including the top and bottom burger bun on the plate.";
                text.text += "When you completed the recipe succesfully you can serve the plate of food on the table to complete the game.";
            }


        }

    }


}
