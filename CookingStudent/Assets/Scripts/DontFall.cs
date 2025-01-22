using UnityEngine;

public class DontFall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Vector3 p = other.transform.position;
        p.y = other.bounds.center.y + 0.1f;
        other.transform.position = p;

        Debug.Log(other);
    }
}
