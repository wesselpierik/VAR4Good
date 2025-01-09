using UnityEngine;

// using System.Collections;
// using System.Collections.Generic;

public class Contamination : MonoBehaviour
{
    /* public states */
    public bool isContaminated = false;
    public bool showContamination = true;
    public bool isSource = false;

    void Start()
    {
        Contaminate(gameObject);
    }

    // void Update()
    // {

    // }

    void Contaminate(GameObject obj)
    {
        if (isContaminated)
        {
            // set contamination
            obj.GetComponent<Contamination>().isContaminated = true;

            // update color
            if (showContamination)
            {
                obj.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isContaminated)
        {
            return;
        }

        Contamination c = other.gameObject.GetComponent<Contamination>();
        if (c != null && !c.isContaminated)
        {
            Contaminate(other.gameObject);
        }
    }
}
