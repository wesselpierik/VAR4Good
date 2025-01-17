using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit;


public class SliceObject : MonoBehaviour
{
    private bool canSlice = true;
    private int counter = 0;

    public Transform startSlicepoint;
    public Transform endSlicepoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

    // Update is called once per frame
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
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicepoint.position - startSlicepoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicepoint.position, planeNormal);

        if(hull != null)
        {
            Contamination targetContamination = target.GetComponent<Contamination>();

            Material crossSectionMaterial = target.GetComponent<Renderer>().material;

            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);
            upperHull.AddComponent<XRGrabInteractable>();
            upperHull.layer = target.layer;

            upperHull.AddComponent<Contamination>();
            upperHull.GetComponent<Contamination>().isContaminatedCookable = targetContamination.isContaminatedCookable;
            upperHull.GetComponent<Contamination>().isContaminatedWashable = targetContamination.isContaminatedWashable;


            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);
            lowerHull.AddComponent<XRGrabInteractable>();
            lowerHull.layer = target.layer;

            lowerHull.AddComponent<Contamination>();
            lowerHull.GetComponent<Contamination>().isContaminatedCookable = targetContamination.isContaminatedCookable;
            lowerHull.GetComponent<Contamination>().isContaminatedWashable = targetContamination.isContaminatedWashable;
            

            Destroy(target);
            counter--;
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
            Debug.Log(counter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((LayerMask.GetMask("Sliceable") & (1 << other.gameObject.layer)) > 0)
        {
            counter--;
            if (counter == 0)
                canSlice = true;

            Debug.Log(counter);
        }
    }
}

