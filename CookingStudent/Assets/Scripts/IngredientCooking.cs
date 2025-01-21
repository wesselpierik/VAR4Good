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
        GetComponent<MeshFilter>().mesh.subMeshCount++;

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
        GlobalStateManager.Instance.CookObject(name, timer);
    }

    private void Burn()
    {
        if (!isBurnt) GlobalStateManager.Instance.AddScore(-5);
        isBurnt = true;
        r.materials[1].color = burntColor;
        GlobalStateManager.Instance.DisplayScore();
    }

    private void OnTriggerExit(Collider item)
    {
        if (item.CompareTag("Pan"))
        {
            isCooking = false;
        }
    }
}
