using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoveZone : MonoBehaviour
{
    public Transform snapPoint;
    private void OnTriggerEnter(Collider item)
    {
        if (item.CompareTag("Pan"))
        {
            item.transform.position = snapPoint.position;
            item.transform.rotation = snapPoint.rotation;

            item.GetComponent<XRGrabInteractable>().attachTransform = null;

            PanLogic panLogic = item.GetComponent<PanLogic>();
            if (panLogic != null)

            {
                panLogic.SetPan(true);
            }
        }
    }

    private void OnTriggerExit(Collider item)
    {
        if (item.CompareTag("Pan"))
        {
            item.GetComponent<XRGrabInteractable>().attachTransform = transform.Find("GrabPoint");

            PanLogic panLogic = item.GetComponent<PanLogic>();

            if (panLogic != null)
            {
                panLogic.SetPan(false);
            }
        }
    }
}
