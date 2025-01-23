using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit;
using JetBrains.Annotations;


public class SliceObject : MonoBehaviour
{
    private bool canSlice = true;
    private int counter = 0;

    public Transform startSlicepoint;
    public Transform endSlicepoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

    void FixedUpdate()
    {
        if (canSlice)
        {
            bool hasHit = Physics.Linecast(startSlicepoint.position, endSlicepoint.position, out RaycastHit hit, sliceableLayer);

            // Debug.Log(hasHit);

            if (hasHit)
            {
                canSlice = false;
                GameObject target = hit.transform.gameObject;
                Slice(target);
            }
        }
    }



    private void PrepareHull(GameObject target, GameObject hull)
    {

        SetupSlicedComponent(hull);
        hull.AddComponent<XRGrabInteractable>();
        hull.name = target.name;
        hull.layer = target.layer;
        hull.tag = target.tag;
        hull.GetComponent<Rigidbody>().mass = 100;


        // TODO: should also copy fields
        IngredientCooking targetIngredientCooking = target.GetComponent<IngredientCooking>();
        if (targetIngredientCooking != null)
        {
            hull.AddComponent<IngredientCooking>();
            IngredientCooking i = hull.GetComponent<IngredientCooking>();
            i.cookingTime = targetIngredientCooking.cookingTime;
            i.burningTime = targetIngredientCooking.burningTime;
        }

        Contamination targetContamination = target.GetComponent<Contamination>();
        if (targetContamination != null)
        {
            hull.AddComponent<Contamination>();
            Contamination c = hull.GetComponent<Contamination>();
            c.isContaminatedCookable = targetContamination.isContaminatedCookable;
            c.isContaminatedWashable = targetContamination.isContaminatedWashable;
            hull.AddComponent<Outline>();
        }
    }


    public void Slice(GameObject target)
    {
        // destroy because it will break otherwise
        Destroy(target.GetComponent<Outline>());

        string objectName = target.name;

        // Create parent node for sliced children.
        GameObject parentNode = GameObject.Find(objectName + "Parent");
        if (parentNode == null)
        {
            parentNode = new GameObject(objectName + "Parent");
            parentNode.transform.position = target.transform.position;
            parentNode.transform.rotation = target.transform.rotation;
        }

        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicepoint.position - startSlicepoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicepoint.position, planeNormal);

        if (hull != null)
        {
            Material crossSectionMaterial = target.GetComponent<Renderer>().material;


            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);

            PrepareHull(target, upperHull);
            PrepareHull(target, lowerHull);

            upperHull.transform.SetParent(parentNode.transform);
            lowerHull.transform.SetParent(parentNode.transform);

            GlobalStateManager.Instance.SliceObject(objectName);
            if (GlobalStateManager.Instance.isRecipeComplete())
            {
                Debug.Log("Recipe is complete");
            }


            Destroy(target);
            counter--;
        }
        else
        {
            Debug.LogWarning("Hull is null");
        }

    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((LayerMask.GetMask("Sliceable") & (1 << other.gameObject.layer)) > 0)
        {
            counter++;
        }
        Debug.Log(counter);
    }

    private void OnTriggerExit(Collider other)
    {
        if ((LayerMask.GetMask("Sliceable") & (1 << other.gameObject.layer)) > 0)
        {
            counter--;
            if (counter == 0)
            {
                canSlice = true;
            }
        }
        Debug.Log(counter);
    }
}
