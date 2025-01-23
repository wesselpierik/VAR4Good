using UnityEngine;


public class Decontamination : MonoBehaviour
{
    public bool decontaminateWashable = true;
    public bool decontaminateCookable = false;


    void OnTriggerStay(Collider other)
    {
        Contamination c = other.gameObject.GetComponent<Contamination>();
        if (c != null && c.IsContaminated())
        {
            c.Decontaminate(decontaminateWashable, decontaminateCookable);
        }
    }
}
