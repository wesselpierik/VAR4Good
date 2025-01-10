using UnityEngine;

public class Contamination : MonoBehaviour
{
    /* public states */
    public bool isContaminated = false;

    // this should be in a global settings scripts probably...
    public bool showContamination = true;
    public DecontaminationType decontaminationType = DecontaminationType.Washable;

    private originalMaterial

    void Start()
    {
        // if (isContaminated)
        // {
        //     Contaminate();
        // }

        if (isContaminated && showContamination)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    // Contaminate self
    void Contaminate()
    {
        isContaminated = true;
        if (showContamination)
        {
            // if is hand:
            if (gameObject.tag == "Hand") {
                GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(1.0f, 0.5f, 0.0f);
            }
            else {
                GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 0.0f);
            }
        }
    }

    public void Decontaminate(DecontaminationType sourceDecontaminationType)
    {
        if (decontaminationType != sourceDecontaminationType) { return; }
        isContaminated = false;

        // TODO: reset the color to original state instead of hardcolor color
        if (gameObject.tag == "Hand") {
            GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;

        }
        else {
            GetComponent<Renderer>().material.color = Color.green;
        }


    }


    // improve the code quality
    void OnCollisionEnter(Collision other)
    {
        if (!isContaminated) { return; }

        Contamination c = other.gameObject.GetComponent<Contamination>();
        if (c != null && !c.isContaminated)
        {
            c.Contaminate();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (!isContaminated) { return; }

        Contamination c = other.gameObject.GetComponent<Contamination>();
        if (c != null && !c.isContaminated)
        {
            c.Contaminate();
        }
    }
}
