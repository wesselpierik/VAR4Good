using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private bool isCooking = false;
    private bool isBurnt = false;

    [SerializeField]
    public float cookingTime = 2.0f; // In Seconds
    [SerializeField]
    public float burningTime = 5.0f; // In Seconds
    private float timer = 0f;
    private Outline outline;


    private void Start()
    {
        outline = GetComponent<Outline>();
        if (burningTime <= cookingTime)
        {
            Debug.LogWarning($"Burning time ({burningTime}s) should be greater than cooking time ({cookingTime}s) on {gameObject.name}!");
        }
    }

    public void StartCooking()
    {
        if (!isCooking && !isBurnt)
        {
            isCooking = true;
            timer = 0f;
        }
    }

    private void Update()
    {
        if (isCooking && !isBurnt)
        {
            timer += Time.deltaTime;
            if (timer >= burningTime)
            {
                Burn();
                isCooking = false;
            }
            else if (timer >= cookingTime)
            {
                Cook();
            }
        }
    }

    private void Cook()
    {
        isBurnt = false;
        UpdateMaterial();
        Debug.Log($"{gameObject.name} cooked");

    }

    private void Burn()
    {
        isBurnt = true;
        UpdateMaterial();
        Debug.Log($"{gameObject.name} burnt");

    }

    private void UpdateMaterial()
    {
        if (!outline)
        {
            Debug.Log("Outline is null");
        }

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = isBurnt ? Color.red : Color.green;
    }


    private void OnTriggerExit(Collider item)
    {
        if (item.CompareTag("Pan"))
        {
            isCooking = false;
            Debug.Log($"{gameObject.name} stopped cooking");
        }
    }
}
