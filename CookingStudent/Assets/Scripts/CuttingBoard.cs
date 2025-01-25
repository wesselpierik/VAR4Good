using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class CuttingBoard : MonoBehaviour
{
    //private GameObject ingredientParent;
    private bool isCutting = false;
    public string assetFolderPath = "Ingredients";

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("==================");
        // Debug.Log($"Touched! Cutting: {isCutting}");

        // check if collision is not null and check if target has ingredient tag
        GameObject ob = collision.gameObject;
        if (isCutting || collision == null || !ob.CompareTag("Ingredient")) return;

        // Debug.Log($"Object: {ob.name}");

        // get the ingredient class from the global list
        // check if target count is 0
        Ingredient ingredient = GetIngredient(ob);
        if (ingredient == null || ingredient.TargetCount == 0 || ingredient.CurrentCount == ingredient.TargetCount) return;

        // Debug.Log($"Ingredient target count: {ingredient.TargetCount}");
        // Debug.Log("Set isCuttin to true");

        isCutting = true;

        // attach the sliceable layer and remove the interactable
        Destroy(ob.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>());
        ob.layer = 6; // Layer number of Sliceable.
    }


    private void OnCollisionExit(Collision collision)
    {
        // re-add the interactable
        // disable slice.
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
        if (!isCutting) return false;

        Ingredient ingredient = GetIngredient(parent);


        // return if not an ingredient in the list or not done
        if (ingredient == null || ingredient.CurrentCount < ingredient.TargetCount) return false;

        // Debug.Log("cutting done, deleting now");

        isCutting = false;
        Destroy(parent);


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

        slicedIngredient.transform.parent = this.gameObject.transform;

        MeshCollider meshCollider = slicedIngredient.AddComponent<MeshCollider>();
        meshCollider.convex = true;

        slicedIngredient.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        slicedIngredient.transform.localPosition = new Vector3(0, 0, 0);

        slicedIngredient.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>().movementType = UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.MovementType.VelocityTracking;

        slicedIngredient.tag = "Ingredient";

        return true;
    }
}
