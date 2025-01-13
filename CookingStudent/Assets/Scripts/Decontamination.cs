using UnityEngine;


public class Decontamination : MonoBehaviour
{
    public bool decontaminateWashable = false;
    public bool decontaminateCookable = false;


    void OnTriggerEnter(Collider other)
    {
        Contamination c = other.gameObject.GetComponent<Contamination>();
        if (c != null && c.IsContaminated())
        {
            c.Decontaminate(decontaminateWashable, decontaminateCookable);
        }
    }
}
