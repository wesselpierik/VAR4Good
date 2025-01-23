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

        other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}