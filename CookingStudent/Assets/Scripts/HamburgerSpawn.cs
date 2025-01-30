using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HamburgerSpawn : MonoBehaviour
{

    HashSet<string> objectsOnPlate = new HashSet<string>();
    HashSet<string> hamburgerIngredients = new HashSet<string>();

    bool isDone = false;

    void Start()
    {
        HamburgerIngredients();
    }

    private void HamburgerIngredients()
    {
        hamburgerIngredients.Add("food_ingredient_bun_bottom");
        hamburgerIngredients.Add("food_ingredient_bun_top");
        hamburgerIngredients.Add("food_ingredient_burger_uncooked");
        hamburgerIngredients.Add("food_ingredient_lettuce_slice(Clone)");
        hamburgerIngredients.Add("food_ingredient_cheese_slice(Clone)");
        hamburgerIngredients.Add("food_ingredient_onion_slice(Clone)");
        hamburgerIngredients.Add("food_ingredient_tomato_slice(Clone)");
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Ingredient") || isDone) return;

        Debug.Log($"collsion name {other.gameObject.name}");

        bool contaminated = other.gameObject.GetComponent<Contamination>().IsContaminated();
        // Debug.Log($"contamination {contaminated}");


        if (other.gameObject.GetComponent<IngredientCooking>() != null)
        {
            bool isCooked = other.gameObject.GetComponent<IngredientCooking>().isDone;
            Debug.Log($"cooked {isCooked}");

            bool isBurnt = other.gameObject.GetComponent<IngredientCooking>().isBurnt;
            Debug.Log($"burnt {isBurnt}");

            if (!contaminated && isCooked && !isBurnt)
            {
                objectsOnPlate.Add(other.gameObject.name);
            }

        }
        else
        {
            if (!contaminated)
            {
                objectsOnPlate.Add(other.gameObject.name);
            }
        }

        if (CheckPlate())
        {
            DeleteIngredients();
            SpawnBurger();
            isDone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectsOnPlate.Remove(other.gameObject.name);
    }

    private bool CheckPlate()
    {
        return objectsOnPlate.SetEquals(hamburgerIngredients);
    }


    private void DeleteIngredients()
    {
        foreach (string obj in objectsOnPlate)
        {
            GameObject deleteObj = GameObject.Find(obj);
            Destroy(deleteObj);
        }
    }

    private void SpawnBurger()
    {
        string assetPath = "IngredientPrefabs/food_burger";
        GameObject assetPrefab = Resources.Load<GameObject>(assetPath);

        if (assetPrefab == null)
        {
            Debug.LogWarning($"Can't find {assetPath} prefab.");
            return;
        }

        // spawn prefab
        GameObject burger = Instantiate(assetPrefab);

        //slicedIngredient.transform.parent = gameObject.transform;

        MeshCollider meshCollider = burger.AddComponent<MeshCollider>();
        meshCollider.convex = true;

        //slicedIngredient.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

        //slicedIngredient.transform.localPosition = new Vector3(0, 0, 0);

        Vector3 p = transform.position;
        p.y += 0.1f;
        burger.transform.position = p;

        burger.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>().movementType = UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.MovementType.VelocityTracking;
        burger.AddComponent<Contamination>();
    }
}
