using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoveZone : XRSocketInteractor
{
    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        if (args.interactable.transform.tag == "Pan") {
            XRBaseInteractable interactable = args.interactable;

            interactable.GetComponent<CustomXRGrabInteractable>().attachTransform = interactable.transform.Find("SnapTransform");

            base.OnSelectEntered(args);
        }

    }

    protected override void OnSelectExited(SelectExitEventArgs args) {
        if (args.interactable.transform.tag == "Pan") {
            XRBaseInteractable interactable = args.interactable;

            interactable.GetComponent<CustomXRGrabInteractable>().attachTransform = interactable.transform.Find("GrabPoint");

            base.OnSelectExited(args);
        }
    }
}
