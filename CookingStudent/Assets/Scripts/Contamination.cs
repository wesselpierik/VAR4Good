using UnityEngine;

public class Contamination : MonoBehaviour
{
    /* public states */
    public bool isContaminatedCookable = false;
    public bool isContaminatedWashable = false;

    public bool mustBeWashed = false;

    [SerializeField] private bool forceHideContamination = false;

    public bool canSpreadContamination = true;
    public bool canReceiveContamination = true;



    // Hardcode
    private Color originalColor = Color.white;
    private Color washableColor = Color.blue;
    private Color cookableColor = Color.red;
    private Color washableCookableColor = new Color(0.75f, 0.0f, 1.0f);

    private bool isHand;
    private bool showContamination; // this should be in a global settings scripts probably...

    void Start()
    {
        isHand = CompareTag("Hand");
        showContamination = GlobalSettingsManager.Instance.GetShowContamination();

        // add outline realtime, but hands have them baked into the prefab
        if (GetOutline() == null && !isHand && !forceHideContamination)
        {
            gameObject.AddComponent<Outline>();
        }

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
        Outline outline = GetOutline();
        if (!showContamination || outline == null) return;

        outline.OutlineMode = IsContaminated() ? Outline.Mode.OutlineVisible : Outline.Mode.OutlineHidden;

        if (isContaminatedWashable && isContaminatedCookable)
        {
            outline.OutlineColor = washableCookableColor;
        }
        else if (isContaminatedWashable)
        {
            outline.OutlineColor = washableColor;
        }
        else if (isContaminatedCookable)
        {
            outline.OutlineColor = cookableColor;
        }
        else
        {
            outline.OutlineColor = originalColor;
        }

    }

    void Contaminate(bool contaminateWashable, bool contaminateCookable)
    {
        if (!canReceiveContamination) return;

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
        // Debug.Log($"{name}, {other.transform.name}");
    }

    void AttemptContamination(Contamination c)
    {
        if (canSpreadContamination && IsContaminated() && c != null)
        {
            c.Contaminate(isContaminatedWashable, isContaminatedCookable);
        }
    }
}
