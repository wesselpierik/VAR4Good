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

            // remove transform to snap correctly
            item.GetComponent<XRGrabInteractable>().attachTransform = null;

            PanLogic panLogic = item.GetComponent<PanLogic>();
            if (panLogic != null)
            {
                Debug.Log("Pan entered stove zone");
                panLogic.SetPan(true);
            }
        }
    }

    private void OnTriggerExit(Collider item)
    {
        if (item.CompareTag("Pan"))
        {
            Debug.Log(item.transform.Find("GrabPoint"));
            item.GetComponent<XRGrabInteractable>().attachTransform = item.transform.Find("GrabPoint");
            PanLogic panLogic = item.GetComponent<PanLogic>();

            if (panLogic != null)
            {
                panLogic.SetPan(false);
            }
        }
    }
}
