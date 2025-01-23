using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRGrabInteractable : XRGrabInteractable
{
    private bool hand = true; // RIGHT HAND

    public override Transform GetAttachTransform(IXRInteractor interactor) {
        if ((interactor.transform.parent.name == "Left Controller" && hand == true) || (interactor.transform.parent.name == "Right Controller" && hand == false)) {
            Vector3 newLocalPosition = attachTransform.localPosition;
            newLocalPosition.z = -newLocalPosition.z;
            attachTransform.localPosition = newLocalPosition;
            hand = !hand;
        }

        return base.GetAttachTransform(interactor);
    }

    protected override void OnSelectExited(SelectExitEventArgs args) {
        IXRSelectInteractable obj = args.interactableObject;
        Rigidbody rb = obj.transform.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        base.OnSelectExited(args);
    }
}
