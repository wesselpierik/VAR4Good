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

            Debug.Log(hasHit);

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
        Contamination targetContamination = target.GetComponent<Contamination>();

        SetupSlicedComponent(hull);
        hull.name = target.name;
        hull.layer = target.layer;
        hull.tag = target.tag;

        hull.AddComponent<XRGrabInteractable>();

        // TODO: should also copy fields
        hull.AddComponent<IngredientCooking>();

        if (targetContamination == null) return;

        hull.AddComponent<Contamination>();
        hull.GetComponent<Contamination>().isContaminatedCookable = targetContamination.isContaminatedCookable;
        hull.GetComponent<Contamination>().isContaminatedWashable = targetContamination.isContaminatedWashable;
        hull.AddComponent<Outline>();
    }


    public void Slice(GameObject target)
    {
        Debug.Log("Slice");
        // destroy because it will break otherwise
        Destroy(target.GetComponent<Outline>());

        string objectName = target.name;

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

            GlobalStateManager.Instance.SliceObject(objectName);
            if (GlobalStateManager.Instance.isRecipeComplete())
            {
                Debug.Log("Recipe is complete");
            }


            Destroy(target);
            counter--;
        }
        else {
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
            Debug.Log($"Enter: {counter}");
        }
    }

    private void OnTriggerExit(Collider other) {
        if ((LayerMask.GetMask("Sliceable") & (1 << other.gameObject.layer)) > 0) {
            Debug.Log(other);
            counter--;
            Debug.Log($"Exit: {counter}");
            if (counter == 0) {
                canSlice = true;
            }
        }
    }
}
