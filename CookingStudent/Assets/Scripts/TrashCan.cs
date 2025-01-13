using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        Destroy(collision.gameObject);
    }
}
