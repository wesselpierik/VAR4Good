using UnityEngine;
using System.Collections.Generic;

public class TrashCan : MonoBehaviour
{
    // public List<string> itemsThrownAway = new List<string>();
    private void OnCollisionEnter(Collision collision)
    {
        // Dont delete tools
        if (collision.gameObject.CompareTag("Pan") || collision.gameObject.CompareTag("Knife")) return;

        // string objectName = collision.gameObject.name;
        // itemsThrownAway.Add(objectName);
        // Debug.Log($"Object Thrown Away: {objectName}");
        GlobalStateManager.Instance.AddScore(-1);
        // GlobalStateManager.Instance.DisplayScore();
        GlobalStateManager.Instance.TrashCount();
        GlobalStateManager.Instance.TrashObject(collision.transform.name);

        Destroy(collision.gameObject);
    }
}
