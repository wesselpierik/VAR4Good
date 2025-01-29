using UnityEngine;
using System.Linq;

public class IngredientCooking : MonoBehaviour
{
    private AudioPlayer audioPlayer;

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

    public void StopCooking() {
        Debug.Log("We exit the pan");
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
            Debug.Log("We are cooking");
            timer += Time.deltaTime;

            Debug.Log($"timer {timer}");
            Debug.Log($"burning time {burningTime}");
            Debug.Log($"cooking time {cookingTime}");
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

        GlobalStateManager.Instance.BurnObject();
        GlobalStateManager.Instance.AddScore(-5);
        GlobalStateManager.Instance.DisplayScore();

        audioPlayer.Play(2);
    }
}
