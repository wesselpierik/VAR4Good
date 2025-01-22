using UnityEngine;
using System.Linq;

public class IngredientCooking : MonoBehaviour
{
    private bool isCooking = false;
    private bool isBurnt = false;

    private bool isDone = false;

    private Color doneColor = new Color(0.0f, 1.0f, 0.0f, 0.75f);
    private Color burntColor = new Color(0.2f, 0.2f, 0.0f, 0.975f);

    [SerializeField]
    public float cookingTime = 2.0f; // In Seconds
    [SerializeField]
    public float burningTime = 5.0f; // In Seconds
    private float timer = 0f;

    private Renderer r;

    private void Awake()
    {
        r = GetComponent<Renderer>();

        /* Add material */
        GetComponent<MeshFilter>().mesh.subMeshCount++; // fix the submesh count
        var materials = r.sharedMaterials.ToList();
        Material m = Instantiate(Resources.Load("M_Cook", typeof(Material)) as Material);
        materials.Add(m);
        r.materials = materials.ToArray();

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
            if (timer >= burningTime && !isBurnt)
            {
                Burn();
            }
            else if (timer >= cookingTime && !isDone)
            {
                Cook();
            }
        }
    }

    private void Cook()
    {
        isDone = true;
        r.materials[1].color = doneColor;
        GlobalStateManager.Instance.CookObject(name, timer);

        Contamination c = GetComponent<Contamination>();
        if (c != null && c.IsContaminated())
        {
            c.Decontaminate(false, true);
        }
    }

    private void Burn()
    {
        isCooking = false;
        GlobalStateManager.Instance.AddScore(-5);
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
