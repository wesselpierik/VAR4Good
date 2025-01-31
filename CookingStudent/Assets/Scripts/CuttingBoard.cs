using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class CuttingBoard : MonoBehaviour
{
    public string assetFolderPath = "Ingredients";

    void OnCollisionEnter(Collision collision)
    {
        // check if collision is not null and check if parent has ingredient tag
        GameObject ob = collision.gameObject;
        if (!ob.CompareTag("Ingredient")) return;


        // get the ingredient class from the global list
        // check if parent count is 0
        Ingredient ingredient = GetIngredient(ob);

        Debug.Log(ingredient);

        if (ingredient == null || ingredient.TargetCount == 0 || ingredient.CurrentCount == ingredient.TargetCount) return;

        // attach the sliceable layer and remove the interactable
        Destroy(ob.GetComponent<CustomXRGrabInteractable>());
        ob.layer = 6; // Layer number of Sliceable.
    }


    private void OnCollisionExit(Collision collision)
    {
        // re-add the interactable
        // disable slice.

        GameObject ob = collision.gameObject;
        if (collision == null || !ob.CompareTag("Ingredient")) return;

        ob.AddComponent<CustomXRGrabInteractable>();
        ob.GetComponent<XRGrabInteractable>().movementType = XRGrabInteractable.MovementType.VelocityTracking;
        ob.GetComponent<XRGrabInteractable>().interactionLayers = 1 << 1;
        ob.layer = 0;
    }


    private Ingredient GetIngredient(GameObject ob)
    {
        List<Ingredient> ingredientList = GlobalStateManager.Instance.recipeList;
        Ingredient ingredient = ingredientList.Find(i => i.ObjectName == ob.name);

        if (ingredient == null)
        {
            Debug.LogWarning($"{ob.name} Is not in ingredient list");
        }

        return ingredient;
    }

    // returns true if cutting is done, false otherwise
    public bool Cut(GameObject parent)
    {
        Ingredient ingredient = GetIngredient(parent);

        // return if not an ingredient in the list or not done
        if (ingredient == null || ingredient.CurrentCount < ingredient.TargetCount) return false;

        // Get prefab
        string assetPath = $"{assetFolderPath}/{parent.name}_slice";
        GameObject assetPrefab = Resources.Load<GameObject>(assetPath);

        if (assetPrefab == null)
        {
            Debug.LogWarning($"Can't find {parent.name} slice prefab.");
            return false;
        }

        // spawn prefab
        GameObject slicedIngredient = Instantiate(assetPrefab);

        slicedIngredient.AddComponent<BoxCollider>();


        Vector3 p = transform.position;
        p.y += 0.1f;
        slicedIngredient.transform.position = p;

        slicedIngredient.AddComponent<CustomXRGrabInteractable>().movementType = XRGrabInteractable.MovementType.VelocityTracking;
        slicedIngredient.GetComponent<CustomXRGrabInteractable>().interactionLayers = 1 << 1;

        slicedIngredient.tag = "Ingredient";

        bool parentIsContaminatedCookable = false;
        bool parentIsContaminatedWashable = false;

        foreach (Transform child in parent.transform)
        {
            GameObject ob = child.gameObject;

            Contamination childContamination = GetComponent<Contamination>();

            if (childContamination != null)
            {
                parentIsContaminatedCookable = parentIsContaminatedCookable || childContamination.isContaminatedCookable;
                parentIsContaminatedWashable = parentIsContaminatedWashable || childContamination.isContaminatedWashable;
            }
        }

        slicedIngredient.AddComponent<Contamination>();
        Contamination c = slicedIngredient.GetComponent<Contamination>();
        c.isContaminatedCookable = parentIsContaminatedCookable;
        c.isContaminatedWashable = parentIsContaminatedWashable;
        slicedIngredient.AddComponent<Outline>();

        Destroy(parent);

        return true;
    }
}
