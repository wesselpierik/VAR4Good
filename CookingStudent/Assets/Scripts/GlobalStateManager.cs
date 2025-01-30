using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GlobalStateManager : MonoBehaviour
{
    public GlobalScore globalScore;
    public List<Ingredient> recipeList;
    public RecipeText recipeText;

    private static GlobalStateManager _instance;

    public static GlobalStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GlobalStateManager>();
                if (_instance == null)
                {
                    Debug.LogError("There is no GlobalStateManager found");
                }
            }

            return _instance;
        }
    }

    private void Start()
    {
        if (globalScore == null)
        {
            Debug.LogWarning("globalScore is null");
        }
        globalScore.ResetScore();
    }

    public void AddScore(int amount)
    {
        if (globalScore == null)
        {
            Debug.LogWarning("globalScore is null");
        }
        globalScore.AddScore(amount);
    }

    public void DisplayScore()
    {
        Debug.Log(globalScore.GetScore());
    }

    public string SliceObject(string objectName)
    {
        Ingredient ingredient = recipeList.FirstOrDefault(i => i.ObjectName == objectName);
        if (ingredient != null)
        {
            string ret = ingredient.UpdateIngredientProgress(0, 0);

            recipeText.RecipeTextAdd();

            return ret;
        }

        return null;
    }

    public string CookObject(string objectName, float timer)
    {
        Ingredient ingredient = recipeList.FirstOrDefault(i => i.ObjectName == objectName);
        if (ingredient != null)
        {
            string ret = ingredient.UpdateIngredientProgress(1, timer);
            recipeText.RecipeTextAdd();
            return ret;
        }

        return null;
    }

    public void BurnObject()
    {
        recipeText.RecipeTextAdd();
    }

    public void TrashObject(string objectName) {
        // food_ingredient_lettuce_slice(Clone)
        int action = -1;

        if (objectName.Substring(objectName.Length - 13) == "_slice(Clone)") {
            objectName = objectName.Substring(0, objectName.Length - 13);
            action = -2;
        }

        Ingredient ingredient = recipeList.FirstOrDefault(i => i.ObjectName == objectName);

        if (ingredient != null)
        {
            string ret = ingredient.UpdateIngredientProgress(action, 0);
            recipeText.RecipeTextAdd();
        }
    }

    public bool isRecipeComplete()
    {
        foreach (Ingredient ingredient in recipeList)
        {
            if (!ingredient.IsIngredientComplete())
            {
                return false;
            }
        }

        return true;
    }


    public void TrashCount()
    {
        globalScore.TrashCount();
    }

    public void ContaminationCount()
    {
        globalScore.ContaminationCount();
    }

    public void BurnCount()
    {
        globalScore.BurnCount();
    }
}
