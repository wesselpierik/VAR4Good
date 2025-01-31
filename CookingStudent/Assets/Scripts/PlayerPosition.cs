using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        GetComponent<CharacterController>().center = new Vector3(0f, 1f, 0f);
    }
}
