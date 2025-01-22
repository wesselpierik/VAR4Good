using UnityEngine;

public class DontFall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Vector3 p = other.transform.position;
        p.y = other.bounds.size.y / 2 + 0.05f;
        other.transform.position = p;

        Debug.Log(other);
    }
}
