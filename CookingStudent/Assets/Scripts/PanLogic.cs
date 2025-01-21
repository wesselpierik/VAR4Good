using UnityEngine;

public class PanLogic : MonoBehaviour
{
    private bool onStove = false;

    public void SetPan(bool action)
    {
        onStove = action;
    }

    private void OnTriggerEnter(Collider item)
    {
        if (item.CompareTag("Ingredient"))
        {
            IngredientCooking ingredient = item.GetComponent<IngredientCooking>();

            if (ingredient && onStove)
            {
                ingredient.StartCooking();
            }
        }
    }
}
