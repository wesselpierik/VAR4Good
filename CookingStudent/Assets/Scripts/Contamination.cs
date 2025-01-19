using UnityEngine;

public class Contamination : MonoBehaviour
{
    /* public states */
    public bool isContaminatedCookable = false;
    public bool isContaminatedWashable = false;

    public bool mustBeWashed = false;

    public bool showContamination = true; // this should be in a global settings scripts probably...

    // Hardcode
    private Color originalColor = Color.white;
    private Color washableColor = Color.blue;
    private Color cookableColor = Color.red;
    private Color washableCookableColor = new Color(0.75f, 0.0f, 1.0f);

    private bool isHand;

    void Start()
    {
        isHand = CompareTag("Hand");

        if (IsContaminated())
            UpdateMaterial();
    }

    public bool IsContaminated()
    {
        return isContaminatedWashable || isContaminatedCookable;
    }

    Outline GetOutline()
    {
        // GetComponentInChildren<Outline>() doesn't work on start, so we have to retrieve on demand.
        return isHand ? GetComponentInChildren<Outline>() : GetComponent<Outline>();
    }

    void UpdateMaterial()
    {
        if (!showContamination) return;

        GetOutline().OutlineMode = IsContaminated() ? Outline.Mode.OutlineVisible : Outline.Mode.OutlineHidden;

        if (isContaminatedWashable && isContaminatedCookable)
        {
            GetOutline().OutlineColor = washableCookableColor;
        }
        else if (isContaminatedWashable)
        {
            GetOutline().OutlineColor = washableColor;
        }
        else if (isContaminatedCookable)
        {
            GetOutline().OutlineColor = cookableColor;
        }
        else
        {
            GetOutline().OutlineColor = originalColor;
        }

    }

    void Contaminate(bool contaminateWashable, bool contaminateCookable)
    {
        // update contamination status
        isContaminatedWashable = isContaminatedWashable || contaminateWashable;
        isContaminatedCookable = isContaminatedCookable || contaminateCookable;

        UpdateMaterial();
    }

    public void Decontaminate(bool decontaminateWashable, bool decontaminateCookable)
    {
        // If must be washed, then both types of contamination are removed only if washed.
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

            /* Only decontaminate if it's not washable.*/
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
