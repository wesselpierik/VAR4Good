using UnityEngine;

public enum DecontaminationType { Washable, Cookable, None };

public class Decontamination : MonoBehaviour
{
    public DecontaminationType decontaminationType = DecontaminationType.Washable;

    void OnTriggerEnter(Collider other)
    {
        Contamination c = other.gameObject.GetComponent<Contamination>();
        if (c != null && c.isContaminated)
        {
            c.Decontaminate(DecontaminationType.Washable);
        }
    }
}
