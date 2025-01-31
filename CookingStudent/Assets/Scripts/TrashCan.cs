using UnityEngine;
using System.Collections.Generic;

public class TrashCan : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Dont delete tools
        if (collision.gameObject.CompareTag("Pan") || collision.gameObject.CompareTag("Knife")) return;

        GlobalStateManager.Instance.AddScore(-1);
        GlobalStateManager.Instance.TrashCount();
        GlobalStateManager.Instance.TrashObject(collision.transform.name);

        Destroy(collision.gameObject);
    }
}
