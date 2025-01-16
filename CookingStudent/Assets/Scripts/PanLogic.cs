using UnityEngine;

public class PanLogic : MonoBehaviour
{
    private bool onStove = false;

    public void SetPan(bool action)
    {
        onStove = action;
        Debug.Log($"Cook zone status: {action}");
    }

    private void OnTriggerEnter(Collider item)
    {
        if (item.CompareTag("Ingredient"))
        {
            Ingredient ingredient = item.GetComponent<Ingredient>();
            Debug.Log("Found item with ingredient tag");

            if (ingredient != null && onStove)
            {
                Debug.Log("Cooking");
                ingredient.StartCooking();
            }
        }
    }
}
