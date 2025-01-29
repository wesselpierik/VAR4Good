using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HamburgerSpawn : MonoBehaviour
{

    HashSet<string> objectsOnPlate = new HashSet<string>();
    HashSet<string> hamburgerIngredients = new HashSet<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HamburgerIngredients();
    }

    private void HamburgerIngredients()
    {
        hamburgerIngredients.Add("food_ingredient_bun_bottom");
        hamburgerIngredients.Add("food_ingredient_bun_top");
        //hamburgerIngredients.Add("food_ingredient_burger_uncooked");
        hamburgerIngredients.Add("food_ingredient_lettuce_slice(Clone)");
        hamburgerIngredients.Add("food_ingredient_cheese_slice(Clone)");
        hamburgerIngredients.Add("food_ingredient_onion_slice(Clone)");
        hamburgerIngredients.Add("food_ingredient_tomato_slice(Clone)");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"collsion name {collision.gameObject.name}");

        bool contaminated = collision.gameObject.GetComponent<Contamination>().IsContaminated();
        Debug.Log($"contamination {contaminated}");


        if (collision.gameObject.GetComponent<IngredientCooking>() != null)
        {
            bool isCooked = collision.gameObject.GetComponent<IngredientCooking>().isDone;
            Debug.Log($"cooked {isCooked}");

            bool isBurnt = collision.gameObject.GetComponent<IngredientCooking>().isBurnt;
            Debug.Log($"burnt {isBurnt}");

            if (!contaminated && isCooked && !isBurnt)
            {
                objectsOnPlate.Add(collision.gameObject.name);
            }

        }
        else
        {
            if (!contaminated)
            {
                objectsOnPlate.Add(collision.gameObject.name);
            }
        }

        bool checkPlate = CheckPlate();

        if (checkPlate)
        {
            DeleteIngredients();
            SpawnBurger();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        objectsOnPlate.Remove(collision.gameObject.name);
    }

    private bool CheckPlate()
    {
        bool checkPlate = objectsOnPlate.SetEquals(hamburgerIngredients);
        Debug.Log(checkPlate);
        return checkPlate;
    }


    private void DeleteIngredients()
    {
        foreach(string obj in objectsOnPlate)
        {
            GameObject deleteObj = GameObject.Find(obj);
            Destroy(deleteObj);
        }
    }

        

    private void SpawnBurger()
    {
        string assetPath = "Ingredients/food_burger";
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
