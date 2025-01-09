using UnityEngine;


public class Contamination : MonoBehaviour
{
    /* public states */
    public bool isContaminated = false;
    public bool isDecontaminatable = true;

    // this should be a global settings scripts probably...
    public bool showContamination = true;


    void Start()
    {
        if (isContaminated)
        {
            Contaminate();
        }
    }

    // Contaminate self
    void Contaminate()
    {
        isContaminated = true;
        if (showContamination)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void Decontaminate()
    {
        if (!isDecontaminatable) { return; }
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
