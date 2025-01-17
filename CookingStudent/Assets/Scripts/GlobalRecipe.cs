using System;
using UnityEngine;


[CreateAssetMenu(fileName = "GlobalRecipe", menuName = "Scriptable Objects/GlobalRecipe")]
public class GlobalRecipe : ScriptableObject
{
    [SerializeField]
    private int cookingState;

    [SerializeField]
    private int cuts;

    public void ResetRecipe()
    {
        cookingState = 0;
        cuts = 0;
    }

    public void UpdateCookingState()
    {
        cookingState++;
    }

    public void UpdateCutsState()
    {
        
        cuts++;
    }

    public int GetCookingState()
    {
        return cookingState;
    }

    public int GetCutsState()
    {
        return cuts;
    }
}
