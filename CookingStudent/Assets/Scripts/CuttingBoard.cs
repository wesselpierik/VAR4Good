using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CuttingBoard : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ingredient")
        {

            Destroy(collision.gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>());
            // Layer number of Sliceable.
            collision.gameObject.layer = 6;
        }
    }
}

