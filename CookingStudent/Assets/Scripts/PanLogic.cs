using UnityEngine;

public class PanLogic : MonoBehaviour
{
    private bool onStove = false;

    public void SetPan(bool action)
    {
        onStove = action;
        // Debug.Log($"Cook zone status: {action}");
    }

    private void OnTriggerEnter(Collider item)
    {
        Debug.Log(item);
        Debug.Log(onStove);
        if (item.CompareTag("Ingredient"))
        {
            IngredientCooking ingredient = item.GetComponent<IngredientCooking>();

            if (ingredient && onStove)
            {
                // Debug.Log("Cooking");
                ingredient.StartCooking();
            }
        }
    }
}
