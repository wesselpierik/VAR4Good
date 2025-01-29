using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoveZone : XRSocketInteractor
{
    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        Debug.Log(args.interactable);
        if (args.interactable.transform.CompareTag("Pan")) {
            XRBaseInteractable interactable = args.interactable;

            interactable.GetComponent<CustomXRGrabInteractable>().attachTransform = interactable.transform.Find("SnapTransform");
            Debug.Log($"{interactable.GetComponent<CustomXRGrabInteractable>().attachTransform.localPosition}");


            PanLogic panLogic = interactable.GetComponent<PanLogic>();
            if (panLogic != null) {
                panLogic.SetPan(true);
            } else {
                Debug.LogWarning("The pan has no panLogic");
            }

            base.OnSelectEntered(args);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args) {
        if (args.interactable.transform.CompareTag("Pan")) {
            XRBaseInteractable interactable = args.interactable;

            interactable.GetComponent<CustomXRGrabInteractable>().attachTransform = interactable.transform.Find("GrabPoint");

            PanLogic panLogic = interactable.GetComponent<PanLogic>();
            if (panLogic != null) {
                panLogic.SetPan(false);
            } else {
                Debug.LogWarning("The pan has no panLogic");
            }

            base.OnSelectExited(args);
        }
    }
}
