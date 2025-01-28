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

            item.transform.SetParent(this.transform);
            // item.GetComponent<Collider>().enabled = false;
            item.GetComponent<Rigidbody>().isKinematic = true;

            Debug.Log(item.GetComponent<Collider>().excludeLayers);

            item.GetComponent<Collider>().excludeLayers = 1 << 9;

            CustomXRGrabInteractable grabShift = item.GetComponent<CustomXRGrabInteractable>();
            grabShift.PanTrigger(true);

            if (ingredient && onStove)
            {
                ingredient.StartCooking();
            }
        }
    }
}
