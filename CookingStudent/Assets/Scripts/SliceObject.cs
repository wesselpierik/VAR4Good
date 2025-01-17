using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit;
using JetBrains.Annotations;
using static GlobalRecipe;


public class SliceObject : MonoBehaviour
{
    private bool canSlice = true;
    private int counter = 0;

    public Transform startSlicepoint;
    public Transform endSlicepoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

    private void Start()
    {
        RecipeList recipeList = GetComponent<RecipeList>();
        recipeList.OnRecipeEvent += OnRecipeUpdated;
    }

    private void OnRecipeUpdated(Recipe recipe)
    {
        Debug.Log($"Updated Recipe: {recipe.ObjectName} ({recipe.CurrentCount}/{recipe.TargetCount})");
    }

    void FixedUpdate()
    {
        if (canSlice)
        {
            bool hasHit = Physics.Linecast(startSlicepoint.position, endSlicepoint.position, out RaycastHit hit, sliceableLayer);

            if (hasHit)
            {
                canSlice = false;
                GameObject target = hit.transform.gameObject;
                Slice(target);
            }
        }
    }

    public void Slice(GameObject target)
    {
        string objectName = target.name;

        Debug.Log("Slice!");
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicepoint.position - startSlicepoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicepoint.position, planeNormal);

        if(hull != null)
        {
            Material crossSectionMaterial = target.GetComponent<Renderer>().material; 

            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);
            upperHull.AddComponent<XRGrabInteractable>();
            upperHull.layer = target.layer;
            upperHull.name = target.name;

            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);
            lowerHull.AddComponent<XRGrabInteractable>();
            lowerHull.layer = target.layer;
            lowerHull.name = target.name;

            Destroy(target);
            counter--;

            RecipeList recipeList = GetComponent<RecipeList>();
            recipeList.UpdateRecipeProgress(objectName);

            // Check if recipe is complete
            if (recipeList.IsRecipeComplete())
            {
                Debug.Log("Recipe Complete!");
            }
        }

    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((LayerMask.GetMask("Sliceable") & (1 << other.gameObject.layer)) > 0)
        {
            counter++;
            //Debug.Log(counter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((LayerMask.GetMask("Sliceable") & (1 << other.gameObject.layer)) > 0)
        {
            counter--;
            if (counter == 0)
                canSlice = true;

            //Debug.Log(counter);
        }
    }
}

