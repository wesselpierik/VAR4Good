using UnityEngine;
using System.Collections.Generic;

public class TrashCan : MonoBehaviour
{
    public List<string> itemsThrownAway = new List<string>();
    private void OnCollisionEnter(Collision collision)
    {
        string objectName = collision.gameObject.name;
        itemsThrownAway.Add(objectName);
        Debug.Log($"Object Thrown Away: {objectName}");
        Destroy(collision.gameObject);
    }
}
