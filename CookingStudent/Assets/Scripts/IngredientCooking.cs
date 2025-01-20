using UnityEditor.SpeedTree.Importer;
using UnityEditor.UI;
using UnityEngine;

public class IngredientCooking : MonoBehaviour
{
    private bool isCooking = false;
    private bool isBurnt = false;

    private Color doneColor = new Color(0.0f, 1.0f, 0.0f, 0.75f);
    private Color burntColor = new Color(0.2f, 0.2f, 0.0f, 0.975f);

    [SerializeField]
    public float cookingTime = 2.0f; // In Seconds
    [SerializeField]
    public float burningTime = 5.0f; // In Seconds
    private float timer = 0f;

    private Renderer r;

    private void Start()
    {
        r = GetComponent<Renderer>();

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
        if (isCooking)
        {
            timer += Time.deltaTime;
            if (timer >= burningTime)
            {
                Burn();
            }
            else if (timer >= cookingTime)
            {
                Cook();
            }
        }
    }

    private void Cook()
    {
        r.materials[1].color = doneColor;
        GlobalStateManager.Instance.CookObject(name);
        // Debug.Log($"{gameObject.name} cooked");
    }

    private void Burn()
    {
        isBurnt = true;
        r.materials[1].color = burntColor;
        GlobalStateManager.Instance.AddScore(-5);
        GlobalStateManager.Instance.DisplayScore();
        // Debug.Log($"{gameObject.name} burnt");
    }

    private void OnTriggerExit(Collider item)
    {
        if (item.CompareTag("Pan"))
        {
            isCooking = false;
            // Debug.Log($"{gameObject.name} stopped cooking");
        }
    }
}
