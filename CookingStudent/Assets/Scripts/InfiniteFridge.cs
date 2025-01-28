using UnityEngine;

public class InfiniteFridge : MonoBehaviour {
    void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Ingredient")) {
            return;
        }

        Transform spawnPoint = other.gameObject.transform.Find("spawnPoint");

        Vector3 newPos = spawnPoint.localPosition + this.transform.position;

        GameObject newObject = Instantiate(other.gameObject, newPos, Quaternion.identity);

        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = true;

        Contamination contamination = other.gameObject.GetComponent<Contamination>();
        contamination.canReceiveContamination = true;

        Renderer r = newObject.GetComponent<Renderer>();
        Material[] materials = r.materials;

        r.materials = materials[..3];

        other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}