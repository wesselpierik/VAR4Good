using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private bool isCooking = false;
    [SerializeField]
    public float cookingTime = 2.0f; // In Seconds
    private float timer = 0f;

    [SerializeField]
    private Material cookedMaterial;
    private Renderer ingredientRenderer;

    private void Start()
    {
        ingredientRenderer = GetComponent<Renderer>();
    }

    public void StartCooking()
    {
        if (!isCooking)
        {
            isCooking = true;
            timer = 0f;
        }
    }

    private void Update()
    {
        if (isCooking)
        {
            timer += Time.deltaTime;
            if (timer >= cookingTime)
            {
                Cook();
                isCooking = false;
            }
        }
    }

    private void Cook()
    {
        if (cookedMaterial != null)
        {
            ingredientRenderer.material = cookedMaterial;
        }

        Collider[] colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            if (!collider.isTrigger)
            {
                collider.enabled = true;
            }
        }
        Debug.Log($"{gameObject.name} is cooked!");
    }
}
