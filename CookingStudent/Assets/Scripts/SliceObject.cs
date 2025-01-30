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
    private bool isEmpty = true;

    public Transform startSlicepoint;
    public Transform endSlicepoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public CuttingBoard cuttingBoard;

    private AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = GetComponent<AudioPlayer>();

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer not found!");
        }

        if (cuttingBoard == null)
        {
            Debug.LogWarning("NO CUTTINGBOARD ATTACHED!");
        }
    }

    void FixedUpdate()
    {
        isEmpty = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((LayerMask.GetMask("Sliceable") & (1 << other.gameObject.layer)) > 0)
        {
            isEmpty = false;
        }

    }

    void Update()
    {
        if (isEmpty && !canSlice)
        {
            canSlice = true;
        }

        if (canSlice)
        {
            bool hasHit = Physics.Linecast(startSlicepoint.position, endSlicepoint.position, out RaycastHit hit, sliceableLayer);

            if (hasHit)
            {
                if (audioPlayer != null)
                {
                    audioPlayer.Play();
                }

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

    private Color32 InsideColor(string target)
    {
        switch (target)
        {
            case "food_ingredient_lettuce":
                return new Color32(173, 172, 66, 255);
            case "food_ingredient_tomato":
                return new Color32(192, 73, 59, 255);
            case "food_ingredient_onion":
                return new Color32(88, 81, 71, 255);
            case "food_ingredient_cheese":
                return new Color32(202, 173, 107, 255);
            default:
                return new Color32(128, 128, 128, 255);
        }
    }

    public void Slice(GameObject target)
    {
        // destroy because it will break otherwise
        Destroy(target.GetComponent<Outline>());

        string objectName = target.name;

        // Create parent node for sliced children.
        GameObject parentNode = target.transform.parent.gameObject;
        if (parentNode == null || parentNode.name != objectName)
        {
            parentNode = new GameObject(objectName);
            parentNode.transform.position = target.transform.position;
            parentNode.transform.rotation = target.transform.rotation;
        }

        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicepoint.position - startSlicepoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicepoint.position, planeNormal);

        if (hull != null)
        {
            Material m = Instantiate(Resources.Load("M_IngredientInside", typeof(Material)) as Material);
            m.color = InsideColor(target.name);
            Material crossSectionMaterial = m;

            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);

            PrepareHull(target, upperHull);
            PrepareHull(target, lowerHull);

            upperHull.transform.SetParent(parentNode.transform);
            lowerHull.transform.SetParent(parentNode.transform);

            GlobalStateManager.Instance.SliceObject(objectName);


            if (GlobalStateManager.Instance.isRecipeComplete())
                Debug.Log("Recipe is complete");

            Destroy(target);


            if (cuttingBoard == null) return;

            // tell the cuttingboard we sliced a specific ingredient
            cuttingBoard.Cut(parentNode);
        }
        else
            Debug.LogWarning("Hull is null");

    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;

    }
}
