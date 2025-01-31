using UnityEngine;
using System.Linq;

public class IngredientCooking : MonoBehaviour
{
    public AudioPlayer audioPlayer;

    public bool isCooking = false;
    public bool isBurnt = false;

    public bool isDone = false;

    private Color doneColor = new Color32(54, 39, 36, 200);
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
        audioPlayer = GetComponent<AudioPlayer>();
        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer not found!");
        }

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

    public void StopCooking()
    {
        isCooking = false;
        audioPlayer.Stop();
    }

    public void StartCooking()
    {
        if (!isCooking && !isBurnt)
        {
            isCooking = true;
            timer = 0f;

            audioPlayer.Play(0);
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

        r.sharedMaterials[1].SetColor("_BaseColor", doneColor);

        Contamination c = GetComponent<Contamination>();
        if (c != null && c.IsContaminated())
        {
            c.Decontaminate(false, true);
        }

        GlobalStateManager.Instance.CookObject(name, timer);

        audioPlayer.PlayOneShot(1);
    }

    private void Burn()
    {
        isCooking = false;
        isBurnt = true;

        r.sharedMaterials[1].SetColor("_BaseColor", burntColor);

        Contamination c = GetComponent<Contamination>();
        if (c != null && c.IsContaminated())
        {
            c.Decontaminate(false, true);
        }

        GlobalStateManager.Instance.AddScore(-15);
        GlobalStateManager.Instance.DisplayScore();
        GlobalStateManager.Instance.BurnCount();

        audioPlayer.Play2(2);
    }
}
