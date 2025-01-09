using UnityEngine;


public class Contamination : MonoBehaviour
{
    /* public states */
    public bool isContaminated = false;

    // this should be in a global settings scripts probably...
    public bool showContamination = true;
    public DecontaminationType decontaminationType = DecontaminationType.Washable;




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
            GetComponent<Renderer>().material.color = new Color(1.0f, 0.64f, 0.0f);
        }
    }

    public void Decontaminate(DecontaminationType sourceDecontaminationType)
    {
        if (decontaminationType != sourceDecontaminationType) { return; }
        isContaminated = false;

        // TODO: reset the color to original state instead of hardcolor color
        GetComponent<Renderer>().material.color = Color.green;
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isContaminated) { return; }

        Contamination c = other.gameObject.GetComponent<Contamination>();
        if (c != null && !c.isContaminated)
        {
            c.Contaminate();
        }
    }
}
