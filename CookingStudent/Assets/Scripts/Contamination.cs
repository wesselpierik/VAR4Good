using UnityEngine;

public class Contamination : MonoBehaviour
{
    /* public states */
    public bool isContaminated = false;

    // this should be in a global settings scripts probably...
    public bool showContamination = true;
    public DecontaminationType decontaminationType = DecontaminationType.Washable;

    private Color contaminationColor = Color.red;
    // new Color(1.0f, 0.5f, 0.0f)
    private Color originalColor;

    void Start()
    {
        originalColor = GetMaterial().color;
        Debug.Log(originalColor);

        if (isContaminated)
            UpdateMaterial(contaminationColor);
    }

    Material GetMaterial()
    {
        return CompareTag("Hand") ? GetComponentInChildren<SkinnedMeshRenderer>().material : GetComponent<Renderer>().material;
    }

    void UpdateMaterial(Color color)
    {
        if (!showContamination) return;
        GetMaterial().color = color;
    }

    // Contaminate self
    void Contaminate(DecontaminationType sourceDecontaminationType)
    {
        //decontaminationType = sourceDecontaminationType; // contaminate the same type?
        isContaminated = true;
        UpdateMaterial(contaminationColor);
    }

    public void Decontaminate(DecontaminationType sourceDecontaminationType)
    {
        if (decontaminationType != sourceDecontaminationType) return;
        Debug.Log(1);
        isContaminated = false;
        UpdateMaterial(originalColor);
    }


    void OnCollisionEnter(Collision other)
    {
        AttemptContamination(other.gameObject.GetComponent<Contamination>());
    }

    void OnTriggerEnter(Collider other)
    {
        AttemptContamination(other.gameObject.GetComponent<Contamination>());
    }

    void AttemptContamination(Contamination c)
    {
        if (isContaminated && c != null && !c.isContaminated)
        {
            c.Contaminate(decontaminationType);
        }
    }
}
