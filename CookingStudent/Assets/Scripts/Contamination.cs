using UnityEngine;

public class Contamination : MonoBehaviour
{
    /* public states */
    public bool isContaminatedCookable = false;
    public bool isContaminatedWashable = false;

    // this should be in a global settings scripts probably...
    public bool showContamination = true;


    private Color contaminationColor = Color.red;
    private Color originalColor;

    public bool IsContaminated() {
        return isContaminatedWashable || isContaminatedCookable;
    }

    void Start()
    {
        // TODO: remove hardcoded hand color
        originalColor = CompareTag("Hand") ? new Color32(0xC4, 0xC4, 0xC4, 0xFF) : GetMaterial().color;

        if (IsContaminated())
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
    void Contaminate(bool contaminateWashable, bool contaminateCookable)
    {
        // update contamination status
        isContaminatedWashable = isContaminatedWashable || contaminateWashable;
        isContaminatedCookable = isContaminatedCookable || contaminateCookable;

        if (IsContaminated())
            UpdateMaterial(contaminationColor);
    }

    public void Decontaminate(bool decontaminateWashable, bool decontaminateCookable)
    {
        if (isContaminatedWashable && decontaminateWashable) {
            isContaminatedWashable = false;
        }

        if (isContaminatedCookable && decontaminateCookable && !isContaminatedWashable) {
            isContaminatedCookable = false;
        }

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
        if (IsContaminated() && c != null && !c.IsContaminated())
        {
            c.Contaminate(isContaminatedWashable, isContaminatedCookable);
        }
    }
}
