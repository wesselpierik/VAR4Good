using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoveZone : XRSocketInteractor
{
    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        if (args.interactable.transform.CompareTag("Pan")) {
            XRBaseInteractable interactable = args.interactable;

            interactable.GetComponent<CustomXRGrabInteractable>().attachTransform = interactable.transform.Find("SnapTransform");

            PanLogic panLogic = interactable.GetComponent<PanLogic>();
            if (panLogic != null) {
                panLogic.SetPan(true);
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
            }

            base.OnSelectExited(args);
        }
    }
}
