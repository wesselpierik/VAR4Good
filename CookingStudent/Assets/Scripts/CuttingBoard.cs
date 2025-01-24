using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEditor;

public class CuttingBoard : MonoBehaviour
{
    private GameObject cuttingIngredient;
    public string assetFolderPath = "Ingredients";


    void Start()
    {
        
    }

    void Update()
    {
        if (cuttingIngredient != null)
        {
            string name = cuttingIngredient.name;
            SpawnNewObjectWhenFinished(name);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            List<Ingredient> ingredientList = GlobalStateManager.Instance.recipeList;

            Ingredient ingredient = ingredientList.Find(i => i.ObjectName == collision.gameObject.name);

            int currentCount = ingredient.CurrentCount;
            int targetCount = ingredient.TargetCount;
            string objectName = ingredient.ObjectName;

            if (collision.gameObject.tag == "Ingredient" && targetCount > 0)
            {
                cuttingIngredient = collision.gameObject;
                Destroy(collision.gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>());
                // Layer number of Sliceable.
                collision.gameObject.layer = 6;
            }
        }
    }

    public void SpawnNewObjectWhenFinished(string ingredientName)
    {
        List<Ingredient> ingredientList = GlobalStateManager.Instance.recipeList;

        Ingredient ingredient = ingredientList.Find(i => i.ObjectName == ingredientName);

        int currentCount = ingredient.CurrentCount;
        int targetCount = ingredient.TargetCount;
        string objectName = ingredient.ObjectName;


        //Debug.Log($"currentcount{currentCount}");
        //Debug.Log($"targetcount {targetCount}");

        if (currentCount >= targetCount)
        {
            GameObject parentNode = GameObject.Find(objectName + "Parent");
            Destroy(parentNode);

            string assetPath = $"{assetFolderPath}/{objectName}_slice";
            GameObject assetPrefab = Resources.Load<GameObject>(assetPath);

            if (assetPrefab != null)
            {
                GameObject slicedIngredient = Instantiate(assetPrefab);
                
                slicedIngredient.transform.parent = this.gameObject.transform;

                //Debug.Log(this);
                //Debug.Log(this.transform.position);

                // Should be in in but breaks it so fix this.
                MeshCollider meshCollider = slicedIngredient.AddComponent<MeshCollider>();
                meshCollider.convex = true;
                slicedIngredient.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                slicedIngredient.transform.localPosition = new Vector3(0, 0, 0);
                slicedIngredient.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>().movementType= UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.MovementType.VelocityTracking;
                slicedIngredient.tag = "Ingredient";
                
            }
        }
    }
}

