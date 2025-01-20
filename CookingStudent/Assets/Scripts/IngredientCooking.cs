using UnityEditor.SpeedTree.Importer;
using UnityEditor.UI;
using UnityEngine;

public class IngredientCooking : MonoBehaviour
{
    private bool isCooking = false;
    private bool isBurnt = false;

    [SerializeField]
    public float cookingTime = 2.0f; // In Seconds
    [SerializeField]
    public float burningTime = 5.0f; // In Seconds
    private float timer = 0f;

    private void Start()
    {
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
        GlobalStateManager.Instance.CookObject(name);
        if (GlobalStateManager.Instance.isRecipeComplete())
        {
            Debug.Log("Recipe is complete");
        }
        Debug.Log($"{gameObject.name} cooked");
    }

    private void Burn()
    {
        isBurnt = true;
        UpdateMaterial();
        GlobalStateManager.Instance.AddScore(-5);
        GlobalStateManager.Instance.DisplayScore();
        Debug.Log($"{gameObject.name} burnt");
    }

    private void UpdateMaterial()
    {

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
