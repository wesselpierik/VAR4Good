using UnityEngine;

public class Decontamination : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Contamination c = other.gameObject.GetComponent<Contamination>();
        if (c != null && c.isContaminated)
        {
            c.Decontaminate();
        }
    }
}
