using UnityEngine;

public class Contamination : MonoBehaviour
{
    /* public states */
    public bool isContaminatedCookable = false;
    public bool isContaminatedWashable = false;

    public bool mustBeWashed = false;

    public bool showContamination = true; // this should be in a global settings scripts probably...

    private Color washableColor = Color.blue;
    private Color cookableColor = Color.red;
    private Color washableCookableColor = new Color(0.5f, 0.0f, 1.0f);

    //set by start
    private Color originalColor;

    private Outline outline;

    private bool isHand;

    public bool IsContaminated()
    {
        return isContaminatedWashable || isContaminatedCookable;
    }

    void Start()
    {
        outline = GetComponent<Outline>();
        // TODO: remove hardcoded hand color
        isHand = CompareTag("Hand");
        // originalColor = isHand ? new Color32(0xC4, 0xC4, 0xC4, 0xFF) : GetMaterial().color;
        originalColor = outline.OutlineColor;

        if (IsContaminated())
            UpdateMaterial();
    }

    // Material GetMaterial()
    // {
    //     return isHand ? GetComponentInChildren<SkinnedMeshRenderer>().material : GetComponent<Renderer>().material;
    // }

    void UpdateMaterial()
    {
        if (!showContamination) return;

        outline.OutlineMode = IsContaminated() ? Outline.Mode.OutlineVisible : Outline.Mode.OutlineHidden;

        if (isContaminatedWashable && isContaminatedCookable)
        {
            outline.OutlineColor = washableCookableColor;
            // GetMaterial().color = washableCookableColor;
        }
        else if (isContaminatedWashable)
        {
            outline.OutlineColor = washableColor;
            // GetMaterial().color = washableColor;
        }
        else if (isContaminatedCookable)
        {
            outline.OutlineColor = cookableColor;
            // GetMaterial().color = cookableColor;
        }
        else
        {
            outline.OutlineColor = originalColor;
            // GetMaterial().color = originalColor;
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
        if (mustBeWashed)
        {
            if (decontaminateWashable)
            {
                isContaminatedWashable = false;
                isContaminatedCookable = false;
            }
        }
        else
        {
            if (isContaminatedWashable && decontaminateWashable)
            {
                isContaminatedWashable = false;
            }

            if (isContaminatedCookable && decontaminateCookable && !isContaminatedWashable)
            {
                isContaminatedCookable = false;
            }
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
