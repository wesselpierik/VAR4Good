using UnityEngine;

public class Contamination : MonoBehaviour
{
    /* public states */
    public bool isContaminatedCookable = false;
    public bool isContaminatedWashable = false;

    public bool showContamination = true; // this should be in a global settings scripts probably...

    private Color washableColor = Color.blue;
    private Color cookableColor = Color.red;
    private Color washableCookableColor = new Color(1.0f, 0.0f, 1.0f);

    private Color originalColor; // set by start

    private string tag = "Hand"

    public bool IsContaminated() {
        return isContaminatedWashable || isContaminatedCookable;
    }

    void Start()
    {
        // TODO: remove hardcoded hand color
        originalColor = CompareTag(tag) ? new Color32(0xC4, 0xC4, 0xC4, 0xFF) : GetMaterial().color;

        if (IsContaminated())
            UpdateMaterial();
    }

    Material GetMaterial()
    {
        return CompareTag(tag) ? GetComponentInChildren<SkinnedMeshRenderer>().material : GetComponent<Renderer>().material;
    }

    void UpdateMaterial()
    {
        if (!showContamination) return;

        if (isContaminatedWashable && isContaminatedCookable) {
            GetMaterial().color = washableCookableColor;
        }
        else if (isContaminatedWashable) {
            GetMaterial().color = washableColor;
        }
        else if (isContaminatedCookable) {
            GetMaterial().color = cookableColor;
        }
        else {
            GetMaterial().color = originalColor;
        }
        
    }

    // Contaminate self
    void Contaminate(bool contaminateWashable, bool contaminateCookable)
    {
        // update contamination status
        isContaminatedWashable = isContaminatedWashable || contaminateWashable;
        isContaminatedCookable = isContaminatedCookable || contaminateCookable;

        UpdateMaterial();
    }

    public void Decontaminate(bool decontaminateWashable, bool decontaminateCookable)
    {
        if (isContaminatedWashable && decontaminateWashable) {
            isContaminatedWashable = false;

            // hands can cleared of both when washing
            if (CompareTag(tag)) {
                isContaminatedCookable = false;
            }
        }

        if (isContaminatedCookable && decontaminateCookable && !isContaminatedWashable ) {
            isContaminatedCookable = false;
        }

        UpdateMaterial();
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
        if (IsContaminated() && c != null)
        {
            c.Contaminate(isContaminatedWashable, isContaminatedCookable);
        }
    }
}
